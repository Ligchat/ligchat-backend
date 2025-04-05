using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.WebhookDto
{
    /// <summary>
    /// DTO para criação de um novo webhook.
    /// </summary>
    public class CreateWebhookRequestDTO
    {
        /// <summary>
        /// Nome do webhook.
        /// Representa o nome pelo qual o webhook é identificado.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL do webhook.
        /// Endereço para o qual as solicitações de webhook serão enviadas.
        /// </summary>
        public string CallbackUrl { get; set; }

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
        public CreateWebhookRequestDTO(
            string name,
            string callbackUrl,
            int? sectorId = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CallbackUrl = callbackUrl ?? throw new ArgumentNullException(nameof(callbackUrl));
            SectorId = sectorId;
        }
    }
}
