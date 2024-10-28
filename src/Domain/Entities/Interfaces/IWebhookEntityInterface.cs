using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de webhook.
    /// </summary>
    public interface IWebhookEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Chave de token para autenticação.
        /// Usada para validar solicitações recebidas no webhook.
        /// </summary>
        string TokenKey { get; set; }

        /// <summary>
        /// URL do webhook.
        /// Endereço para o qual as solicitações de webhook serão enviadas.
        /// </summary>
        string Url { get; set; }
    }
}
