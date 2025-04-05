using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.UserDto
{
    /// <summary>
    /// DTO para a criação de um novo usuário.
    /// </summary>
    public class CreateUserRequestDTO
    {
        /// <summary>
        /// Nome do usuário. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail do usuário. Este campo é obrigatório.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// URL do avatar do usuário. Este campo é opcional.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Número de WhatsApp do usuário. Este campo é obrigatório.
        /// </summary>
        public string PhoneWhatsapp { get; set; }

        /// <summary>
        /// Indica se o usuário é um administrador. Este campo é opcional e padrão é false.
        /// </summary>
        public bool IsAdmin { get; set; } = false; // Define o valor padrão como false

        /// <summary>
        /// Indica se o usuário está ativo. Este campo é opcional e padrão é true.
        /// </summary>
        public bool Status { get; set; } = true; // Define o valor padrão como true

        /// <summary>
        /// ID do usuário que convidou este usuário. Este campo é opcional.
        /// </summary>
        public int? InvitedBy { get; set; }

        /// <summary>
        /// Lista de setores associados ao usuário, incluindo a flag de compartilhamento (IsShared).
        /// </summary>
        public List<SectorDTO>? Sectors { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="phoneWhatsapp">Número de WhatsApp do usuário.</param>
        /// <param name="avatarUrl">URL do avatar do usuário.</param>
        /// <param name="isAdmin">Indica se o usuário é um administrador.</param>
        /// <param name="status">Indica se o usuário está ativo.</param>
        /// <param name="invitedBy">ID do usuário que convidou este usuário.</param>
        /// <param name="sectors">Lista de setores associados ao usuário.</param>
        [JsonConstructor]
        public CreateUserRequestDTO(
            string name,
            string email,
            string phoneWhatsapp,
            string? avatarUrl = null,
            bool isAdmin = false,
            bool status = true,
            int? invitedBy = null,
            List<SectorDTO>? sectors = null)
        {
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
            IsAdmin = isAdmin;
            Status = status;
            InvitedBy = invitedBy;
            Sectors = sectors;
        }
    }
}
