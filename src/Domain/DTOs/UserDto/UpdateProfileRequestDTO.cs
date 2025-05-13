using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.UserDto
{
    /// <summary>
    /// DTO para a atualização apenas do perfil básico de um usuário.
    /// Não inclui campos administrativos como IsAdmin, Status ou Sectors.
    /// </summary>
    public class UpdateProfileRequestDTO
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// URL do avatar do usuário. Este campo é opcional.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Número de WhatsApp do usuário.
        /// </summary>
        public string PhoneWhatsapp { get; set; } = string.Empty;

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        [JsonConstructor]
        public UpdateProfileRequestDTO(
            string name = "",
            string email = "",
            string phoneWhatsapp = "",
            string? avatarUrl = null)
        {
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
        }
    }
} 