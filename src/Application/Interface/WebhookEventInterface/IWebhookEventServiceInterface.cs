using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Domain.DTOs.WebhookEventDto;

namespace LigChat.Backend.Application.Interface.WebhookEventInterface
{
    public interface IWebhookEventServiceInterface
    {
        WebhookEventListResponse GetAll();
        SingleWebhookEventResponse? GetById(int id);
        SingleWebhookEventResponse? Save(CreateWebhookEventRequestDTO webhookEventDto);
        SingleWebhookEventResponse? Update(int id, UpdateWebhookEventRequestDTO webhookEventDto);
        SingleWebhookEventResponse? Delete(int id);
    }
}
