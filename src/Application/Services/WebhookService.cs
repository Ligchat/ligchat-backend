using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Application.Interface.WebhookInterface;
using LigChat.Backend.Data.Interfaces.IRepositories;
using LigChat.Backend.Domain.DTOs.WebhookDto;
using LigChat.Backend.Domain.Entities;
using System;
using System.Linq;

namespace LigChat.Com.Api.Mvc.WebhookMvc.Service
{
    public class WebhookService : IWebhookServiceInterface
    {
        private readonly IWebhookRepositoryInterface _webhookRepository;

        // Construtor que injeta as dependências necessárias
        public WebhookService(IWebhookRepositoryInterface webhookRepository)
        {
            _webhookRepository = webhookRepository;
        }

        /// <summary>
        /// Obtém todos os webhooks e retorna uma resposta com uma lista de webhooks.
        /// </summary>
        public WebhookListResponse GetAll(int sectorId) 
        {
            var webhooks = _webhookRepository.GetAll(sectorId);
            var webhookDtos = webhooks.Select(w => new WebhookViewModel(
                w.Id,
                w.Name,
                string.Empty, // TokenKey
                w.CallbackUrl,
                true, // Status - assumindo ativo por padrão
                w.SectorId
            )).ToList();

            return new WebhookListResponse("Success", "200", webhookDtos);
        }

        /// <summary>
        /// Obtém um webhook pelo ID e retorna uma resposta com os dados do webhook.
        /// </summary>
        public SingleWebhookResponse? GetById(int id)
        {
            var webhook = _webhookRepository.GetById(id);
            if (webhook == null)
            {
                return new SingleWebhookResponse("Webhook not found", "404", null);
            }

            var webhookDto = new WebhookViewModel(
                webhook.Id,
                webhook.Name,
                string.Empty, // TokenKey
                webhook.CallbackUrl,
                true, // Status - assumindo ativo por padrão
                webhook.SectorId
            );

            return new SingleWebhookResponse("Success", "200", webhookDto);
        }

        /// <summary>
        /// Cria um novo webhook com base nos dados fornecidos e retorna uma resposta com o webhook criado.
        /// </summary>
        public SingleWebhookResponse Save(CreateWebhookRequestDTO webhookDto)
        {
            // Valida os dados do webhook
            if (string.IsNullOrWhiteSpace(webhookDto.Name) || string.IsNullOrWhiteSpace(webhookDto.CallbackUrl))
            {
                return new SingleWebhookResponse("Invalid request", "400", null);
            }

            // Cria um novo webhook a partir do DTO
            var webhook = new Webhook
            {
                Name = webhookDto.Name,
                CallbackUrl = webhookDto.CallbackUrl,
                SectorId = webhookDto.SectorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Salva o webhook no repositório
            var savedWebhook = _webhookRepository.Save(webhook);

            // Cria e retorna a resposta
            var responseDto = new WebhookViewModel(
                savedWebhook.Id,
                savedWebhook.Name,
                string.Empty, // TokenKey
                savedWebhook.CallbackUrl,
                true, // Status - assumindo ativo por padrão
                savedWebhook.SectorId
            );

            return new SingleWebhookResponse("Webhook created successfully", "201", responseDto);
        }

        /// <summary>
        /// Atualiza um webhook existente com base nos dados fornecidos e retorna uma resposta com o webhook atualizado.
        /// </summary>
        public SingleWebhookResponse Update(int id, UpdateWebhookRequestDTO webhookDto)
        {
            // Valida os dados do webhook
            if (string.IsNullOrWhiteSpace(webhookDto.Name) || string.IsNullOrWhiteSpace(webhookDto.CallbackUrl))
            {
                return new SingleWebhookResponse("Invalid request", "400", null);
            }

            // Recupera o webhook existente
            var existingWebhook = _webhookRepository.GetById(id);
            if (existingWebhook == null)
            {
                return new SingleWebhookResponse("Webhook not found", "404", null);
            }

            // Atualiza o webhook com os dados do DTO
            existingWebhook.Name = webhookDto.Name;
            existingWebhook.CallbackUrl = webhookDto.CallbackUrl;
            existingWebhook.SectorId = webhookDto.SectorId;
            existingWebhook.UpdatedAt = DateTime.UtcNow;

            // Salva o webhook atualizado no repositório
            var savedWebhook = _webhookRepository.Update(id, existingWebhook);

            // Cria e retorna a resposta
            var responseDto = new WebhookViewModel(
                savedWebhook.Id,
                savedWebhook.Name,
                string.Empty, // TokenKey
                savedWebhook.CallbackUrl,
                true, // Status - assumindo ativo por padrão
                savedWebhook.SectorId
            );

            return new SingleWebhookResponse("Webhook updated successfully", "200", responseDto);
        }

        /// <summary>
        /// Deleta um webhook pelo ID e retorna uma resposta com o webhook deletado.
        /// </summary>
        public SingleWebhookResponse Delete(int id)
        {
            // Deleta o webhook do repositório
            var deletedWebhook = _webhookRepository.Delete(id);
            if (deletedWebhook == null)
            {
                return new SingleWebhookResponse("Webhook not found", "404", null);
            }

            // Cria e retorna a resposta
            var responseDto = new WebhookViewModel(
                deletedWebhook.Id,
                deletedWebhook.Name,
                string.Empty, // TokenKey
                deletedWebhook.CallbackUrl,
                true, // Status - assumindo ativo por padrão
                deletedWebhook.SectorId
            );

            return new SingleWebhookResponse("Webhook deleted successfully", "200", responseDto);
        }
    }
}
