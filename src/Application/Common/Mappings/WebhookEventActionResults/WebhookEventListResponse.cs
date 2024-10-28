using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de eventos de webhook.
    /// </summary>
    public class WebhookEventListResponse : ListResponse<WebhookEventViewModel>
    {
        public WebhookEventListResponse(string message, string code, IEnumerable<WebhookEventViewModel> webhookEvents)
            : base(message, code, webhookEvents)
        {
        }

        public WebhookEventListResponse() : base() { }
    }
}
