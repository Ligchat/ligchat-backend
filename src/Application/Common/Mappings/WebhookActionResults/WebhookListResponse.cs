using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de webhooks.
    /// </summary>
    public class WebhookListResponse : ListResponse<WebhookViewModel>
    {
        public WebhookListResponse(string message, string code, IEnumerable<WebhookViewModel> webhooks)
            : base(message, code, webhooks)
        {
        }

        public WebhookListResponse() : base() { }
    }
}
