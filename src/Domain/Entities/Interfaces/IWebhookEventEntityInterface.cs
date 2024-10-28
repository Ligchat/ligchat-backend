using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de evento de webhook.
    /// </summary>
    public interface IWebhookEventEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Identificador do webhook.
        /// Referencia o webhook ao qual o evento está associado.
        /// </summary>
        int WebhookId { get; set; }

        /// <summary>
        /// Tipo do evento.
        /// Descreve o tipo de evento que ocorreu no webhook.
        /// </summary>
        string EventType { get; set; }

        /// <summary>
        /// Payload do evento.
        /// Contém os dados do evento no formato de carga útil.
        /// </summary>
        string Payload { get; set; }
    }
}
