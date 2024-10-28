namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do evento de webhook na API.
    /// </summary>
    public class WebhookEventViewModel
    {
        // Identificador único do evento de webhook
        public int Id { get; }

        // Nome do webhook
        public string Name { get; }

        // Identificador do webhook
        public int WebhookId { get; }

        // Tipo do evento
        public string EventType { get; }

        // Payload do evento
        public string Payload { get; }

        // Status do evento
        public bool Status { get; }

        // Identificador do setor associado ao fluxo (opcional)
        public int? SectorId { get; }

        // Data de criação do evento de webhook
        public DateTime CreatedAt { get; }

        // Data da última atualização do evento de webhook
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public WebhookEventViewModel()
        {
            Id = 0;
            Name = string.Empty;
            WebhookId = 0;
            EventType = string.Empty;
            Payload = string.Empty;
            Status = false;
            SectorId = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public WebhookEventViewModel(int id, string name, int webhookId, string eventType, string payload, bool status, int? sectorId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            WebhookId = webhookId;
            EventType = eventType;
            Payload = payload;
            Status = status;
            SectorId = sectorId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
