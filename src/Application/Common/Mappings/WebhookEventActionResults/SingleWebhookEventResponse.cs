using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único evento de webhook.
    /// </summary>
    public class SingleWebhookEventResponse : SingleResponse<WebhookEventViewModel>
    {
        public SingleWebhookEventResponse(string message, string code, WebhookEventViewModel webhookEvent)
            : base(message, code, webhookEvent)
        {
        }

        public SingleWebhookEventResponse() : base() { }
    }
}
