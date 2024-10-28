using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único webhook.
    /// </summary>
    public class SingleWebhookResponse : SingleResponse<WebhookViewModel>
    {
        public SingleWebhookResponse(string message, string code, WebhookViewModel webhook)
            : base(message, code, webhook)
        {
        }

        public SingleWebhookResponse() : base() { }
    }
}
