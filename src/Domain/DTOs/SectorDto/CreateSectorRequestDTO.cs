using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.SectorDto
{
    /// <summary>
    /// DTO para a criação de um novo setor.
    /// </summary>
    public class CreateSectorRequestDTO
    {
        /// <summary>
        /// Nome do setor. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do setor. Este campo é obrigatório.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador do usuário responsável pelo setor. Este campo é obrigatório.
        /// </summary>
        public int UserBusinessId { get; set; }

        /// <summary>
        /// Número de telefone associado ao setor. Este campo é opcional.
        /// </summary>
        public string? PhoneNumberId { get; set; }

        /// <summary>
        /// Token de acesso para o setor. Este campo é opcional.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Status do setor. Este campo é opcional.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do setor.</param>
        /// <param name="description">Descrição do setor.</param>
        /// <param name="userBusinessId">Identificador do usuário responsável pelo setor.</param>
        /// <param name="phoneNumberId">Número de telefone associado ao setor.</param>
        /// <param name="accessToken">Token de acesso para o setor.</param>
        /// <param name="status">Status do setor.</param>
        [JsonConstructor]
        public CreateSectorRequestDTO(
            string name,
            string description,
            int userBusinessId,
            string? phoneNumberId = null,
            string? accessToken = null,
            bool? status = null) // Parâmetros opcionais com valor padrão
        {
            Name = name;
            Description = description;
            UserBusinessId = userBusinessId;
            PhoneNumberId = phoneNumberId; // Inicializando o novo campo
            AccessToken = accessToken; // Inicializando o novo campo
            Status = status; // Inicializando o novo campo
        }
    }
}
