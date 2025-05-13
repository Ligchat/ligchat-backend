using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada ao contato.
        /// Referencia a tag que categoriza o contato.
        /// </summary>
        public int? TagId { get; set; }

        /// <summary>
        /// Número de WhatsApp do contato.
        /// Usado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// URL da foto de perfil do WhatsApp.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// E-mail do contato.
        /// Utilizado para comunicação e autenticação do contato.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Notas adicionais sobre o contato.
        /// Opcional. Pode conter informações adicionais ou comentários sobre o contato.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Identificador do setor associado ao contato.
        /// </summary>
        [Required]
        public int SectorId { get; set; }

        /// <summary>
        /// Status do contato.
        /// Indica se o contato está ativo ou não.
        /// </summary>
        [Required]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Identificador do setor associado ao contato.
        /// </summary>
        public int? AiActive { get; set; }

        /// <summary>
        /// Identificador do setor associado ao contato.
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Prioridade do contato.
        /// </summary>
        public string? Priority { get; set; } = "normal";

        /// <summary>
        /// Status do contato.
        /// </summary>
        public string? ContactStatus { get; set; }

        /// <summary>
        /// Identificador do setor associado ao contato.
        /// </summary>
        public bool IsOfficial { get; set; }

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
            string number,
            string? avatarUrl = null,
            string? email = null,
            string? notes = null,
            int sectorId = 0,
            bool isActive = true,
            int? aiActive = null,
            int? assignedTo = null,
            string? priority = "normal",
            bool isOfficial = false)
        {
            Name = name;
            TagId = tagId;
            Number = number;
            AvatarUrl = avatarUrl;
            Email = email;
            Notes = notes;
            SectorId = sectorId;
            IsActive = isActive;
            AiActive = aiActive;
            AssignedTo = assignedTo;
            Priority = priority;
            IsOfficial = isOfficial;
        }
    }
}
