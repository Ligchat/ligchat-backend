using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Application.Interface.WebhookEventInterface;
using LigChat.Backend.Data.Interfaces.IRepositories;
using LigChat.Backend.Domain.DTOs.WebhookEventDto;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Com.Api.Mvc.WebhookEventMvc.Service
{
    public class WebhookEventService : IWebhookEventServiceInterface
    {
        private readonly IWebhookEventRepositoryInterface _webhookEventRepository;

        // Construtor que injeta as dependências necessárias
        public WebhookEventService(IWebhookEventRepositoryInterface webhookEventRepository)
        {
            _webhookEventRepository = webhookEventRepository;
        }

        /// <summary>
        /// Obtém todos os eventos de webhook e retorna uma resposta com uma lista de eventos.
        /// </summary>
        public WebhookEventListResponse GetAll()
        {
            var events = _webhookEventRepository.GetAll();
            var eventDtos = events.Select(e => new WebhookEventViewModel(
                e.Id,
                e.Name,
                e.WebhookId,
                e.EventType,
                e.Payload,
                e.Status,
                e.SectorId,
                e.CreatedAt,
                e.UpdatedAt
            )).ToList(); // Convert to List for better performance in case of large datasets

            return new WebhookEventListResponse("Success", "200", eventDtos);
        }

        /// <summary>
        /// Obtém um evento de webhook pelo ID e retorna uma resposta com os dados do evento.
        /// </summary>
        public SingleWebhookEventResponse? GetById(int id)
        {
            var webhookEvent = _webhookEventRepository.GetById(id);
            if (webhookEvent == null)
            {
                return new SingleWebhookEventResponse("Webhook event not found", "404", null);
            }

            var eventDto = new WebhookEventViewModel(
                webhookEvent.Id,
                webhookEvent.Name,
                webhookEvent.WebhookId,
                webhookEvent.EventType,
                webhookEvent.Payload,
                webhookEvent.Status,
                webhookEvent.SectorId,
                webhookEvent.CreatedAt,
                webhookEvent.UpdatedAt
            );

            return new SingleWebhookEventResponse("Success", "200", eventDto);
        }

        /// <summary>
        /// Cria um novo evento de webhook com base nos dados fornecidos e retorna uma resposta com o evento criado.
        /// </summary>
        public SingleWebhookEventResponse Save(CreateWebhookEventRequestDTO eventDto)
        {
            // Valida os dados do evento
            if (string.IsNullOrWhiteSpace(eventDto.Name) || string.IsNullOrWhiteSpace(eventDto.EventType))
            {
                return new SingleWebhookEventResponse("Invalid request", "400", null);
            }

            // Cria um novo evento a partir do DTO
            var webhookEvent = new WebhookEvent
            {
                Name = eventDto.Name,
                WebhookId = eventDto.WebhookId,
                EventType = eventDto.EventType,
                Payload = eventDto.Payload,
                Status = eventDto.Status,
                SectorId = eventDto.SectorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Salva o evento no repositório
            var savedEvent = _webhookEventRepository.Save(webhookEvent);

            // Cria e retorna a resposta
            var responseDto = new WebhookEventViewModel(
                savedEvent.Id,
                savedEvent.Name,
                savedEvent.WebhookId,
                savedEvent.EventType,
                savedEvent.Payload,
                savedEvent.Status,
                savedEvent.SectorId,
                savedEvent.CreatedAt,
                savedEvent.UpdatedAt
            );

            return new SingleWebhookEventResponse("Webhook event created successfully", "201", responseDto);
        }

        /// <summary>
        /// Atualiza um evento de webhook existente com base nos dados fornecidos e retorna uma resposta com o evento atualizado.
        /// </summary>
        public SingleWebhookEventResponse Update(int id, UpdateWebhookEventRequestDTO eventDto)
        {
            // Valida os dados do evento
            if (string.IsNullOrWhiteSpace(eventDto.Name) || string.IsNullOrWhiteSpace(eventDto.EventType))
            {
                return new SingleWebhookEventResponse("Invalid request", "400", null);
            }

            // Recupera o evento existente
            var existingEvent = _webhookEventRepository.GetById(id);
            if (existingEvent == null)
            {
                return new SingleWebhookEventResponse("Webhook event not found", "404", null);
            }

            // Atualiza o evento com os dados do DTO
            existingEvent.Name = eventDto.Name;
            existingEvent.WebhookId = eventDto.WebhookId;
            existingEvent.EventType = eventDto.EventType;
            existingEvent.Payload = eventDto.Payload;
            existingEvent.Status = eventDto.Status;
            existingEvent.SectorId = eventDto.SectorId;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            // Salva o evento atualizado no repositório
            var savedEvent = _webhookEventRepository.Update(id, existingEvent);

            // Cria e retorna a resposta
            var responseDto = new WebhookEventViewModel(
                savedEvent.Id,
                savedEvent.Name,
                savedEvent.WebhookId,
                savedEvent.EventType,
                savedEvent.Payload,
                savedEvent.Status,
                savedEvent.SectorId,
                savedEvent.CreatedAt,
                savedEvent.UpdatedAt
            );

            return new SingleWebhookEventResponse("Webhook event updated successfully", "200", responseDto);
        }

        /// <summary>
        /// Deleta um evento de webhook pelo ID e retorna uma resposta com o evento deletado.
        /// </summary>
        public SingleWebhookEventResponse Delete(int id)
        {
            // Deleta o evento do repositório
            var deletedEvent = _webhookEventRepository.Delete(id);
            if (deletedEvent == null)
            {
                return new SingleWebhookEventResponse("Webhook event not found", "404", null);
            }

            // Cria e retorna a resposta
            var responseDto = new WebhookEventViewModel(
                deletedEvent.Id,
                deletedEvent.Name,
                deletedEvent.WebhookId,
                deletedEvent.EventType,
                deletedEvent.Payload,
                deletedEvent.Status,
                deletedEvent.SectorId,
                deletedEvent.CreatedAt,
                deletedEvent.UpdatedAt
            );

            return new SingleWebhookEventResponse("Webhook event deleted successfully", "200", responseDto);
        }
    }
}
