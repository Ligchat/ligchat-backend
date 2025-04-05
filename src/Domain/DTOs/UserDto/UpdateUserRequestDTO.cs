using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.UserDto
{
    /// <summary>
    /// DTO para a atualização de um usuário existente.
    /// </summary>
    public class UpdateUserRequestDTO
    {
        /// <summary>
        /// Nome do usuário. Este campo é opcional.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário. Este campo é opcional.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// URL do avatar do usuário. Este campo é opcional.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Número de WhatsApp do usuário. Este campo é opcional.
        /// </summary>
        public string PhoneWhatsapp { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o usuário é um administrador. Este campo é opcional e padrão é false.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Indica se o usuário está ativo. Este campo é opcional e padrão é true.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// ID do usuário que convidou este usuário. Este campo é opcional.
        /// </summary>
        public int? InvitedBy { get; set; }

        /// <summary>
        /// Lista de setores associados ao usuário. Este campo é opcional.
        /// </summary>
        public List<SectorDTO> Sectors { get; set; } = new List<SectorDTO>();

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
        public UpdateUserRequestDTO(
            int? invitedBy,
            string name = "",
            string email = "",
            string phoneWhatsapp = "",
            string? avatarUrl = null,
            bool isAdmin = false,
            bool status = true,
            List<SectorDTO>? sectors = null)
        {
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
            IsAdmin = isAdmin;
            Status = status;
            InvitedBy = invitedBy;
            Sectors = sectors ?? new List<SectorDTO>();
        }
    }

    /// <summary>
    /// DTO para representar um setor associado ao usuário.
    /// </summary>
    public class SectorDTO
    {
        /// <summary>
        /// Identificador do setor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do setor.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o setor é compartilhado.
        /// </summary>
        public bool IsShared { get; set; } = true;
    }
}
