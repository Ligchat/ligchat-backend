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
        /// Chave de token para autenticação.
        /// Usada para validar solicitações recebidas no webhook.
        /// </summary>
        public string TokenKey { get; set; }

        /// <summary>
        /// URL do webhook.
        /// Endereço para o qual as solicitações de webhook serão enviadas.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Status do webhook.
        /// Pode representar diferentes estados como Ativo ou Inativo.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Identificador do setor associado ao webhook.
        /// Este campo é opcional e representa o ID do setor ao qual o webhook pode pertencer.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do webhook.</param>
        /// <param name="tokenKey">Chave de token para autenticação.</param>
        /// <param name="url">URL do webhook.</param>
        /// <param name="status">Status do webhook.</param>
        /// <param name="sectorId">Identificador do setor associado ao webhook.</param>
        [JsonConstructor]
        public CreateWebhookRequestDTO(
            string name, 
            string tokenKey, 
            string url, 
            bool status, 
            int? sectorId = null)
        {
            Name = name;
            TokenKey = tokenKey;
            Url = url;
            Status = status;
            SectorId = sectorId;
        }
    }
}
