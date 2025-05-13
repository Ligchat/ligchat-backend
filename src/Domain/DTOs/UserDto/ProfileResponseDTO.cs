using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.UserDto
{
    /// <summary>
    /// DTO para a resposta do perfil básico de um usuário.
    /// Não inclui campos administrativos como IsAdmin, Status ou Sectors.
    /// </summary>
    public class ProfileResponseDTO
    {
        /// <summary>
        /// ID do usuário.
        /// </summary>
        public int Id { get; set; }

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
        public ProfileResponseDTO(
            int id,
            string name = "",
            string email = "",
            string phoneWhatsapp = "",
            string? avatarUrl = null)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização.
        /// </summary>
        public ProfileResponseDTO() { }
    }
} 