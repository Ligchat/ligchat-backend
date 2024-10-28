using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.WebhookEventDto
{
    /// <summary>
    /// DTO para criação de um novo evento de webhook.
    /// </summary>
    public class CreateWebhookEventRequestDTO
    {
        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador do webhook.
        /// Referencia o webhook ao qual o evento está associado.
        /// </summary>
        public int WebhookId { get; set; }

        /// <summary>
        /// Tipo do evento.
        /// Descreve o tipo de evento que ocorreu no webhook.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Payload do evento.
        /// Contém os dados do evento no formato de carga útil.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Status do evento.
        /// Pode representar diferentes estados como Ativo ou Inativo.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Identificador do setor associado ao evento.
        /// Este campo é opcional e representa o ID do setor ao qual o evento pode pertencer.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do webhook.</param>
        /// <param name="webhookId">Identificador do webhook.</param>
        /// <param name="eventType">Tipo do evento.</param>
        /// <param name="payload">Payload do evento.</param>
        /// <param name="status">Status do evento.</param>
        /// <param name="sectorId">Identificador do setor associado ao evento.</param>
        [JsonConstructor]
        public CreateWebhookEventRequestDTO(
            string name, 
            int webhookId, 
            string eventType, 
            string payload, 
            bool status, 
            int? sectorId = null)
        {
            Name = name;
            WebhookId = webhookId;
            EventType = eventType;
            Payload = payload;
            Status = status;
            SectorId = sectorId;
        }
    }
}
