using System.Collections.Generic;

namespace LigChat.Backend.Application.Common.Mappings.UserActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do usuário na API.
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneWhatsapp { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsAdmin { get; set; }
        public bool Status { get; set; }
        public int? InvitedBy { get; set; }
        public List<SectorInfo> Sectors { get; set; } = new List<SectorInfo>();

        public UserViewModel()
        {
            Name = string.Empty;
            Email = string.Empty;
            PhoneWhatsapp = string.Empty;
        }

        public UserViewModel(int id, string name, string email, string phoneWhatsapp, string? avatarUrl, bool isAdmin, bool status, int? invitedBy = null)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            AvatarUrl = avatarUrl;
            IsAdmin = isAdmin;
            Status = status;
            InvitedBy = invitedBy;
            Sectors = new List<SectorInfo>();
        }
    }

    public class SectorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SectorInfo(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
