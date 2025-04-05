using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Domain.Entities
{
    [Table("setores")]
    public class Sector : ISectorEntityInterface
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome_setor")]
        [Required]
        public string Name { get; set; }

        [Column("descricao_setor")]
        [Required]
        public string Description { get; set; }

        [Column("id_negocio_usuario")]
        public int? UserBusinessId { get; set; }

        [Column("numero")]
        public string PhoneNumberId { get; set; }

        [Column("token_acesso")]
        public string AccessToken { get; set; }

        [Column("google_client_id")]
        public string? GoogleClientId { get; set; }

        [Column("google_api_key")]
        public string? GoogleApiKey { get; set; }

        [Column("token_acesso_oauth2")]
        public string? OAuth2AccessToken { get; set; }

        [Column("refresh_token_oauth2")]
        public string? OAuth2RefreshToken { get; set; }

        [Column("expiracao_token_oauth2")]
        public DateTime? OAuth2TokenExpiration { get; set; }

        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        [Column("status")]
        public bool? Status { get; set; } = true;

        /// <summary>
        /// Lista de relacionamentos com usuários através da tabela UserSector.
        /// </summary>
        public ICollection<UserSector> UserSectors { get; set; } = new List<UserSector>();

        public Sector()
        {
            Name = string.Empty;
            Description = string.Empty;
            PhoneNumberId = string.Empty;
            AccessToken = string.Empty;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public Sector(
            string name,
            string description,
            int? userBusinessId,
            string phoneNumberId,
            string accessToken,
            string googleClientId,
            string googleApiKey,
            string? oauth2AccessToken,
            string? oauth2RefreshToken,
            DateTime? oauth2TokenExpiration)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            UserBusinessId = userBusinessId;
            PhoneNumberId = phoneNumberId ?? throw new ArgumentNullException(nameof(phoneNumberId));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            GoogleClientId = googleClientId ?? throw new ArgumentNullException(nameof(googleClientId));
            GoogleApiKey = googleApiKey ?? throw new ArgumentNullException(nameof(googleApiKey));
            OAuth2AccessToken = oauth2AccessToken;
            OAuth2RefreshToken = oauth2RefreshToken;
            OAuth2TokenExpiration = oauth2TokenExpiration;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
    }
}
