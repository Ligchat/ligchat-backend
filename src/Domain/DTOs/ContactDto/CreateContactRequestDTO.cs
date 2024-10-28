using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.ContactDto
{
    /// <summary>
    /// DTO para criação de um novo contato.
    /// </summary>
    public class CreateContactRequestDTO
    {
        /// <summary>
        /// Nome do contato.
        /// Representa o nome completo ou o nome pelo qual o usuário é conhecido.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada ao contato.
        /// Referencia a tag que categoriza o contato.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Número de WhatsApp do contato.
        /// Usado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// URL da foto de perfil do WhatsApp.
        /// </summary>
        public string? ProfilePicUrl { get; set; }

        /// <summary>
        /// E-mail do contato.
        /// Utilizado para comunicação e autenticação do contato.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Notas adicionais sobre o contato.
        /// Opcional. Pode conter informações adicionais ou comentários sobre o contato.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Identificador do setor associado ao contato.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Status do contato.
        /// Indica se o contato está ativo ou não.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Construtor para inicialização da classe <see cref="CreateContactRequestDTO"/>.
        /// </summary>
        /// <param name="name">Nome do contato.</param>
        /// <param name="tagId">Identificador da etiqueta associada ao contato.</param>
        /// <param name="phoneWhatsapp">Número de WhatsApp do contato.</param>
        /// <param name="profilePicUrl">URL da foto de perfil do WhatsApp.</param>
        /// <param name="email">E-mail do contato.</param>
        /// <param name="notes">Notas adicionais sobre o contato.</param>
        /// <param name="sectorId">Identificador do setor associado ao contato.</param>
        /// <param name="status">Status do contato.</param>
        [JsonConstructor]
        public CreateContactRequestDTO(
            string name,
            int tagId,
                        string email,
            string number,
            string? profilePicUrl = null,
            string? notes = null,
            int? sectorId = null,
            bool status = false)
        {
            Name = name;
            TagId = tagId;
            Number = number;
            ProfilePicUrl = profilePicUrl;
            Email = email;
            Notes = notes;
            SectorId = sectorId;
            Status = status; // Inicializa o status
        }
    }
}
