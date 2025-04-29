using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using System.Linq;
using LigChat.Backend.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.IO;
using LigChat.Backend.Application.Services.Storage;
using Microsoft.EntityFrameworkCore;
using LigChat.Backend.Web.Extensions.Database;
using System.Collections.Generic;
using AutoMapper;
using LigChat.Backend.Application.Common;
using LigChat.Backend.Domain.ViewModels;
using LigChat.Backend.Application.Interface.MessageSchedulingInterface;
using LigChat.Backend.Application.Interface.S3StorageInterface;

namespace LigChat.Backend.Application.Services
{
    public class MessageSchedulingService : IMessageSchedulingServiceInterface
    {
        private readonly IMessageSchedulingRepositoryInterface _repository;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3Service;
        private readonly DatabaseConfiguration _context;

        public MessageSchedulingService(
            IMessageSchedulingRepositoryInterface repository,
            IMapper mapper,
            IS3StorageService s3Service,
            DatabaseConfiguration context)
        {
            _repository = repository;
            _mapper = mapper;
            _s3Service = s3Service;
            _context = context;
        }

        public async Task<Response<List<MessageSchedulingViewModel>>> GetAll(int sectorId)
        {
            try
            {
                var messages = await _repository.GetAll(sectorId);
                var viewModels = _mapper.Map<List<MessageSchedulingViewModel>>(messages);

                return new Response<List<MessageSchedulingViewModel>>
                {
                    Success = true,
                    Message = "Message schedulings retrieved successfully.",
                    StatusCode = 200,
                    Data = viewModels
                };
            }
            catch (Exception ex)
            {
                return new Response<List<MessageSchedulingViewModel>>
                {
                    Success = false,
                    Message = $"Error retrieving message schedulings: {ex.Message}",
                    StatusCode = 500,
                    Data = null
                };
            }
        }

        public async Task<Response<MessageSchedulingViewModel>> GetById(int id)
        {
            try
            {
                var message = await _repository.GetById(id);
                if (message == null)
                {
                    return new Response<MessageSchedulingViewModel>
                    {
                        Success = false,
                        Message = $"Message scheduling with ID {id} not found.",
                        StatusCode = 404,
                        Data = null
                    };
                }

                var viewModel = _mapper.Map<MessageSchedulingViewModel>(message);
                return new Response<MessageSchedulingViewModel>
                {
                    Success = true,
                    Message = "Message scheduling retrieved successfully.",
                    StatusCode = 200,
                    Data = viewModel
                };
            }
            catch (Exception ex)
            {
                return new Response<MessageSchedulingViewModel>
                {
                    Success = false,
                    Message = $"Error retrieving message scheduling: {ex.Message}",
                    StatusCode = 500,
                    Data = null
                };
            }
        }

        public async Task<Response<MessageSchedulingViewModel>> SaveAsync(CreateMessageSchedulingRequestDTO messageDto)
        {
            try
            {
                // Validar se o setor existe
                var sectorExists = await _context.Sectors.AnyAsync(s => s.Id == messageDto.SectorId);
                if (!sectorExists)
                {
                    return new Response<MessageSchedulingViewModel>
                    {
                        Success = false,
                        Message = $"Setor com ID {messageDto.SectorId} não encontrado",
                        StatusCode = 404
                    };
                }

                // Validar se o contato existe
                var contactExists = await _context.Contacts.AnyAsync(c => c.Id == messageDto.ContactId);
                if (!contactExists)
                {
                    return new Response<MessageSchedulingViewModel>
                    {
                        Success = false,
                        Message = $"Contato com ID {messageDto.ContactId} não encontrado",
                        StatusCode = 404
                    };
                }

                var message = _mapper.Map<MessageScheduling>(messageDto);
                message.CreatedAt = DateTime.UtcNow;
                message.UpdatedAt = DateTime.UtcNow;

                if (messageDto.Attachments != null && messageDto.Attachments.Count > 0)
                {
                    foreach (var attachment in messageDto.Attachments)
                    {
                        string base64Content = attachment.Data ?? attachment.Base64Content;
                        if (string.IsNullOrEmpty(base64Content))
                        {
                            continue;
                        }

                        string s3Url = await _s3Service.UploadFileAsync(
                            base64Content,
                            attachment.Name,
                            attachment.Type,
                            attachment.MimeType
                        );

                        var messageAttachment = new MessageAttachment
                        {
                            Type = attachment.Type,
                            FileName = attachment.Name,
                            S3Url = s3Url,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        message.Attachments.Add(messageAttachment);
                    }
                }

                var savedMessage = await _repository.Save(message);
                var viewModel = _mapper.Map<MessageSchedulingViewModel>(savedMessage);

                return new Response<MessageSchedulingViewModel>
                {
                    Success = true,
                    Message = "Message scheduled successfully",
                    Data = viewModel,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                return new Response<MessageSchedulingViewModel>
                {
                    Success = false,
                    Message = $"Error scheduling message: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<Response<MessageSchedulingViewModel>> Update(int id, UpdateMessageSchedulingRequestDTO messageDto)
        {
            try
            {
                // 1. Carregar a mensagem existente
                var existingMessage = await _repository.GetById(id);
                if (existingMessage == null)
                {
                    return new Response<MessageSchedulingViewModel>
                    {
                        Success = false,
                        Message = $"Message scheduling with ID {id} not found.",
                        StatusCode = 404,
                        Data = null
                    };
                }

                // 2. Deletar anexos antigos (se existirem)
                if (existingMessage.Attachments != null && existingMessage.Attachments.Any())
                {
                    // Remover arquivos do S3
                    foreach (var attachment in existingMessage.Attachments)
                    {
                        if (!string.IsNullOrEmpty(attachment.S3Url))
                        {
                            await _s3Service.DeleteFileAsync(attachment.S3Url);
                        }
                    }
                    
                    // Remover anexos do banco via SQL direto
                    await _context.Database.ExecuteSqlRawAsync(
                        "DELETE FROM mensagens_anexos WHERE mensagem_id = {0}", 
                        existingMessage.Id);
                }
                
                // 3. Desconectar a entidade do contexto para evitar problemas de rastreamento
                _context.ChangeTracker.Clear();
                
                // 4. Atualizar a mensagem no banco via SQL direto
                await _context.Database.ExecuteSqlRawAsync(
                    @"UPDATE mensagens_agendadas 
                    SET nome = {0}, mensagem_de_texto = {1}, data_envio = {2}, 
                        contato_id = {3}, setor_id = {4}, status = {5}, 
                        tag_id = {6}, data_atualizacao = {7}
                    WHERE id = {8}",
                    messageDto.Name,
                    messageDto.MessageText,
                    messageDto.SendDate,
                    messageDto.ContactId,
                    messageDto.SectorId,
                    messageDto.Status,
                    messageDto.TagIds ?? string.Empty,
                    DateTime.UtcNow,
                    id);
                
                // 5. Adicionar novos anexos (se enviados)
                if (messageDto.Attachments != null && messageDto.Attachments.Any())
                {
                    foreach (var attachmentDto in messageDto.Attachments)
                    {
                        try
                        {
                            string content = attachmentDto.Base64Content ?? attachmentDto.Data;
                            if (string.IsNullOrEmpty(content))
                            {
                                continue;
                            }
                            
                            string s3Url;
                            
                            // Verificar se o conteúdo é uma URL existente ou um novo conteúdo base64
                            if (content.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                // É um anexo existente, só reutilizar a URL
                                s3Url = content;
                            }
                            else
                            {
                                // É um novo anexo, fazer upload
                                s3Url = await _s3Service.UploadFileAsync(content, attachmentDto.Name, attachmentDto.Type);
                            }
                            
                            var now = DateTime.UtcNow;
                            
                            // Inserir via SQL sem especificar ID
                            await _context.Database.ExecuteSqlRawAsync(
                                @"INSERT INTO mensagens_anexos 
                                (mensagem_id, tipo, nome_arquivo, url_s3, data_criacao, data_atualizacao) 
                                VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                                id, attachmentDto.Type, attachmentDto.Name, 
                                s3Url, now, now);
                        }
                        catch (Exception ex)
                        {
                            // Log do erro mas continuar para outros anexos
                            Console.WriteLine($"Erro ao processar anexo '{attachmentDto.Name}': {ex.Message}");
                        }
                    }
                }
                
                // 6. Recarregar a entidade com seus anexos
                var updatedMessage = await _repository.GetById(id);
                var viewModel = _mapper.Map<MessageSchedulingViewModel>(updatedMessage);

                return new Response<MessageSchedulingViewModel>
                {
                    Success = true,
                    Message = "Message scheduling updated successfully.",
                    StatusCode = 200,
                    Data = viewModel
                };
            }
            catch (Exception ex)
            {
                return new Response<MessageSchedulingViewModel>
                {
                    Success = false,
                    Message = $"Error updating message scheduling: {ex.Message}",
                    StatusCode = 500,
                    Data = null
                };
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var message = await _repository.GetById(id);
                if (message == null)
                {
                    return new Response<bool>
                    {
                        Success = false,
                        Message = "Message not found",
                        StatusCode = 404
                    };
                }

                if (message.Attachments != null)
                {
                    foreach (var attachment in message.Attachments)
                    {
                        if (!string.IsNullOrEmpty(attachment.S3Url))
                        {
                            await _s3Service.DeleteFileAsync(attachment.S3Url);
                        }
                    }
                }

                await _repository.Delete(id);

                return new Response<bool>
                {
                    Success = true,
                    Message = "Message deleted successfully",
                    Data = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = $"Error deleting message: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".txt":
                    return "text/plain";
                case ".mp3":
                    return "audio/mpeg";
                case ".mp4":
                    return "video/mp4";
                case ".wav":
                    return "audio/wav";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
