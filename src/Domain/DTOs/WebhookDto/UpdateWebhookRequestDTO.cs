using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.WebhookDto
{
    /// <summary>
    /// DTO para atualização de um webhook existente.
    /// </summary>
    public class UpdateWebhookRequestDTO
    {
        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// URL do webhook.
        /// Endereço para o qual as solicitações de webhook serão enviadas.
        /// Este campo é opcional.
        /// </summary>
        public string? CallbackUrl { get; set; }

        /// <summary>
        /// Identificador do setor associado ao webhook.
        /// Este campo é opcional e representa o ID do setor ao qual o webhook pode pertencer.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do webhook.</param>
        /// <param name="callbackUrl">URL do webhook.</param>
        /// <param name="sectorId">Identificador do setor associado ao webhook.</param>
        [JsonConstructor]
        public UpdateWebhookRequestDTO(
            string? name = null,
            string? callbackUrl = null,
            int? sectorId = null)
        {
            Name = name;
            CallbackUrl = callbackUrl;
            SectorId = sectorId;
        }
    }
}
