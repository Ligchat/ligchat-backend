using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using System.Linq;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Api.Services.MessageSchedulingService
{
    public class MessageSchedulingService : IMessageSchedulingServiceInterface
    {
        private readonly IMessageSchedulingRepositoryInterface _messageSchedulingRepository;

        public MessageSchedulingService(IMessageSchedulingRepositoryInterface messageSchedulingRepository)
        {
            _messageSchedulingRepository = messageSchedulingRepository;
        }

        public MessageSchedulingListResponse GetAll(int sectorId)
        {
            var messageSchedulings = _messageSchedulingRepository.GetAll(sectorId);
            var messageSchedulingDtos = messageSchedulings.Select(ms => new MessageSchedulingViewModel(
                ms.Id,
                ms.Name,
                ms.MessageText,
                ms.SendDate,
                ms.FlowId,
                ms.SectorId,
                ms.Tags,
                ms.CreatedAt,
                ms.UpdatedAt,
                ms.ImageName,
                ms.FileName,
                ms.ImageAttachment,
                ms.FileAttachment,
                ms.ImageMimeType,
                ms.FileMimeType
            )).ToList();
            return new MessageSchedulingListResponse("Success", "200", messageSchedulingDtos);
        }

        public SingleMessageSchedulingResponse? GetById(int id)
        {
            var messageScheduling = _messageSchedulingRepository.GetById(id);
            if (messageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }
            var messageSchedulingDto = new MessageSchedulingViewModel(
                messageScheduling.Id,
                messageScheduling.Name,
                messageScheduling.MessageText,
                messageScheduling.SendDate,
                messageScheduling.FlowId,
                messageScheduling.SectorId,
                messageScheduling.Tags,
                messageScheduling.CreatedAt,
                messageScheduling.UpdatedAt,
                messageScheduling.ImageName,
                messageScheduling.FileName,
                messageScheduling.ImageAttachment,
                messageScheduling.FileAttachment,
                messageScheduling.ImageMimeType,
                messageScheduling.FileMimeType
            );
            return new SingleMessageSchedulingResponse("Success", "200", messageSchedulingDto);
        }

        public SingleMessageSchedulingResponse Save(CreateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            // Validação manual
            if (string.IsNullOrWhiteSpace(messageSchedulingDto.Name))
            {
                return new SingleMessageSchedulingResponse("Invalid request: Name is required", "400", null);
            }

            // Criação do objeto MessageScheduling
            var messageScheduling = new MessageScheduling
            {
                Name = messageSchedulingDto.Name,
                MessageText = messageSchedulingDto.MessageText,
                FlowId = messageSchedulingDto.FlowId,
                SectorId = messageSchedulingDto.SectorId,
                SendDate = messageSchedulingDto.SendDate,
                Tags = messageSchedulingDto.TagIds,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageName = messageSchedulingDto.ImageName,
                FileName = messageSchedulingDto.FileName,
                ImageAttachment = messageSchedulingDto.ImageAttachment,
                FileAttachment = messageSchedulingDto.FileAttachment,
                ImageMimeType = messageSchedulingDto.ImageMimeType,
                FileMimeType = messageSchedulingDto.FileMimeType
            };

            var savedMessageScheduling = _messageSchedulingRepository.Save(messageScheduling);
            var responseDto = new MessageSchedulingViewModel(
                savedMessageScheduling.Id,
                savedMessageScheduling.Name,
                savedMessageScheduling.MessageText,
                savedMessageScheduling.SendDate,
                savedMessageScheduling.FlowId,
                savedMessageScheduling.SectorId,
                savedMessageScheduling.Tags,
                savedMessageScheduling.CreatedAt,
                savedMessageScheduling.UpdatedAt,
                savedMessageScheduling.ImageName,
                savedMessageScheduling.FileName,
                savedMessageScheduling.ImageAttachment,
                savedMessageScheduling.FileAttachment,
                savedMessageScheduling.ImageMimeType,
                savedMessageScheduling.FileMimeType
            );
            return new SingleMessageSchedulingResponse("Message Scheduling created successfully", "201", responseDto);
        }

        public SingleMessageSchedulingResponse Update(int id, UpdateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            var existingMessageScheduling = _messageSchedulingRepository.GetById(id);
            if (existingMessageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }

            // Atualiza os campos opcionais, se fornecidos.
            if (messageSchedulingDto.Name != null)
                existingMessageScheduling.Name = messageSchedulingDto.Name;

            if (messageSchedulingDto.MessageText != null)
                existingMessageScheduling.MessageText = messageSchedulingDto.MessageText;

            if (!string.IsNullOrEmpty(messageSchedulingDto.SendDate))
                existingMessageScheduling.SendDate = messageSchedulingDto.SendDate;

            if (messageSchedulingDto.FlowId != null)
                existingMessageScheduling.FlowId = messageSchedulingDto.FlowId;

            if (messageSchedulingDto.SectorId.HasValue)
                existingMessageScheduling.SectorId = messageSchedulingDto.SectorId;

            if (messageSchedulingDto.TagIds != null)
                existingMessageScheduling.Tags = messageSchedulingDto.TagIds;

            if (messageSchedulingDto.ImageName != null)
                existingMessageScheduling.ImageName = messageSchedulingDto.ImageName;

            if (messageSchedulingDto.FileName != null)
                existingMessageScheduling.FileName = messageSchedulingDto.FileName;

            if (messageSchedulingDto.ImageAttachment != null)
                existingMessageScheduling.ImageAttachment = messageSchedulingDto.ImageAttachment;

            if (messageSchedulingDto.FileAttachment != null)
                existingMessageScheduling.FileAttachment = messageSchedulingDto.FileAttachment;

            if (messageSchedulingDto.ImageMimeType != null)
                existingMessageScheduling.ImageMimeType = messageSchedulingDto.ImageMimeType;

            if (messageSchedulingDto.FileMimeType != null)
                existingMessageScheduling.FileMimeType = messageSchedulingDto.FileMimeType;

            existingMessageScheduling.UpdatedAt = DateTime.UtcNow;

            var savedMessageScheduling = _messageSchedulingRepository.Update(id, existingMessageScheduling);
            var responseDto = new MessageSchedulingViewModel(
                savedMessageScheduling.Id,
                savedMessageScheduling.Name,
                savedMessageScheduling.MessageText,
                savedMessageScheduling.SendDate,
                savedMessageScheduling.FlowId,
                savedMessageScheduling.SectorId,
                savedMessageScheduling.Tags,
                savedMessageScheduling.CreatedAt,
                savedMessageScheduling.UpdatedAt,
                savedMessageScheduling.ImageName,
                savedMessageScheduling.FileName,
                savedMessageScheduling.ImageAttachment,
                savedMessageScheduling.FileAttachment,
                savedMessageScheduling.ImageMimeType,
                savedMessageScheduling.FileMimeType
            );
            return new SingleMessageSchedulingResponse("Message Scheduling updated successfully", "200", responseDto);
        }

        public SingleMessageSchedulingResponse Delete(int id)
        {
            var deletedMessageScheduling = _messageSchedulingRepository.Delete(id);
            if (deletedMessageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }
            var responseDto = new MessageSchedulingViewModel(
                deletedMessageScheduling.Id,
                deletedMessageScheduling.Name,
                deletedMessageScheduling.MessageText,
                deletedMessageScheduling.SendDate,
                deletedMessageScheduling.FlowId,
                deletedMessageScheduling.SectorId,
                deletedMessageScheduling.Tags,
                deletedMessageScheduling.CreatedAt,
                deletedMessageScheduling.UpdatedAt,
                deletedMessageScheduling.ImageName,
                deletedMessageScheduling.FileName,
                deletedMessageScheduling.ImageAttachment,
                deletedMessageScheduling.FileAttachment,
                deletedMessageScheduling.ImageMimeType,
                deletedMessageScheduling.FileMimeType
            );
            return new SingleMessageSchedulingResponse("Message Scheduling deleted successfully", "200", responseDto);
        }
    }
}
