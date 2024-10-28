using System.Collections.Generic;

namespace LigChat.Backend.Application.Common.Mappings.UserActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do usuário na API.
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneWhatsapp { get; }
        public string? AvatarUrl { get; }

        /// <summary>
        /// Indica se o usuário é um administrador.
        /// </summary>
        public bool IsAdmin { get; } // Propriedade para verificar se o usuário é um administrador

        /// <summary>
        /// Indica se o usuário está ativo.
        /// </summary>
        public bool Status { get; } // Propriedade para verificar se o usuário está ativo

        public UserViewModel(int id, string name, string email, string phoneWhatsapp, string? avatarUrl, bool isAdmin, bool status)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
            IsAdmin = isAdmin; // Inicializa a propriedade IsAdmin
            Status = status; // Inicializa a propriedade Status
        }
    }
}
