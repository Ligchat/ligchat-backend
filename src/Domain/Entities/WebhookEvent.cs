using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de evento de webhook no sistema.
    /// Mapeia a tabela "webhook_events" no banco de dados e herda de BaseEntity.
    /// </summary>
    [Table("webhook_events")]
    public class WebhookEvent : IWebhookEventEntityInterface
    {
        /// <summary>
        /// Identificador único do evento de webhook.
        /// Este campo é a chave primária da tabela.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// </summary>
        [Column("nome")]
        [Required]
        [MaxLength(100)] // Limita o comprimento do nome a 100 caracteres
        public string Name { get; set; }

        /// <summary>
        /// Identificador do webhook.
        /// Referencia o webhook ao qual o evento está associado.
        /// </summary>
        [Column("webhook_id")]
        [Required]
        public int WebhookId { get; set; }

        /// <summary>
        /// Tipo do evento.
        /// Descreve o tipo de evento que ocorreu no webhook.
        /// </summary>
        [Column("tipo_evento")]
        [Required]
        [MaxLength(50)] // Limita o comprimento do tipo de evento a 50 caracteres
        public string EventType { get; set; }

        /// <summary>
        /// Payload do evento.
        /// Contém os dados do evento no formato de carga útil.
        /// </summary>
        [Column("payload")]
        [Required]
        public string Payload { get; set; }

        /// <summary>
        /// Status da ação do fluxo.
        /// Este campo é obrigatório e pode representar diferentes estados como Ativa ou Inativa.
        /// </summary>
        [Column("status")]
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Identificador do setor associado ao fluxo.
        /// Este campo é opcional e representa o ID do setor ao qual o fluxo pertence, se aplicável.
        /// </summary>
        [Column("setor_id")]
        public int? SectorId { get; set; }

        /// <summary>
        /// Data e hora de criação do evento de webhook.
        /// Este campo é utilizado para registrar quando o evento foi criado.
        /// </summary>
        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização do evento de webhook.
        /// Este campo é utilizado para registrar quando o evento foi atualizado pela última vez.
        /// </summary>
        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Construtor padrão da classe WebhookEvent.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public WebhookEvent()
        {
            CreatedAt = DateTime.UtcNow; // Inicializa CreatedAt com a data e hora atuais em UTC
            UpdatedAt = DateTime.UtcNow; // Inicializa UpdatedAt com a data e hora atuais em UTC
        }

        /// <summary>
        /// Construtor da classe WebhookEvent que inicializa a entidade com valores específicos.
        /// </summary>
        /// <param name="name">Nome do webhook.</param>
        /// <param name="webhookId">Identificador do webhook.</param>
        /// <param name="eventType">Tipo do evento.</param>
        /// <param name="payload">Payload do evento.</param>
        public WebhookEvent(string name, int webhookId, string eventType, string payload)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo.");
            WebhookId = webhookId;
            EventType = eventType ?? throw new ArgumentNullException(nameof(eventType), "O tipo de evento não pode ser nulo.");
            Payload = payload ?? throw new ArgumentNullException(nameof(payload), "O payload não pode ser nulo.");
            CreatedAt = DateTime.UtcNow; // Inicializa CreatedAt com a data e hora atuais em UTC
            UpdatedAt = DateTime.UtcNow; // Inicializa UpdatedAt com a data e hora atuais em UTC
        }
    }
}
