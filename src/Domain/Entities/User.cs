using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;
using tests_.src.Domain.Entities;

namespace LigChat.Backend.Domain.Entities
{
    [Table("usuarios")]
    public class User : IUserEntityInterface
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Column("avatar_url")]
        public string? AvatarUrl { get; set; }

        [Column("phone_whatsapp")]
        [Required(ErrorMessage = "O número de WhatsApp é obrigatório.")]
        public string PhoneWhatsapp { get; set; } = string.Empty;

        [Column("setor_id")]
        public int? SectorId { get; set; }

        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("is_admin")]
        [Required(ErrorMessage = "A indicação de administrador é obrigatória.")]
        public bool IsAdmin { get; set; } = false; // Default to false

        [Column("verification_code")]
        public string? VerificationCode { get; set; }

        [Column("verification_code_expires_at")]
        public DateTime? VerificationCodeExpiresAt { get; set; }

        // Nova propriedade para armazenar o ID do usuário que convidou
        [Column("invited_by")]
        public int? InvitedBy { get; set; } // Pode ser nulo se não houver um convidador

        // Relationship with UserPermission
        public ICollection<UserPermission> UserPermissions { get; set; }

        public User()
        {
        }

        public User(string name, string email, string phoneWhatsapp, int? sectorId = null, bool status = true, bool isAdmin = false, string? avatarUrl = null, int? invitedBy = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo ou vazio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "O e-mail não pode ser nulo ou vazio.");

            if (string.IsNullOrWhiteSpace(phoneWhatsapp))
                throw new ArgumentNullException(nameof(phoneWhatsapp), "O número de WhatsApp não pode ser nulo ou vazio.");

            Name = name;
            Email = email;
            PhoneWhatsapp = phoneWhatsapp;
            SectorId = sectorId;
            Status = status;
            IsAdmin = isAdmin; // Define se o usuário é admin
            AvatarUrl = avatarUrl;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
            InvitedBy = invitedBy; // Inicializa a propriedade de convidador
        }
    }
}
