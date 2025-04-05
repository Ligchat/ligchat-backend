using System;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.SectorDto
{
    /// <summary>
    /// DTO para a atualização das informações de um setor.
    /// </summary>
    public class UpdateSectorRequestDTO
    {
        /// <summary>
        /// Nome do setor. Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição do setor. Este campo é opcional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Número de telefone associado ao setor. Este campo é opcional.
        /// </summary>
        public string? PhoneNumberId { get; set; }

        /// <summary>
        /// Token de acesso para o setor. Este campo é opcional.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Status do setor. Este campo é opcional.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// Client ID do Google para integrações de API. Este campo é opcional.
        /// </summary>
        public string? GoogleClientId { get; set; }

        /// <summary>
        /// API Key do Google para integrações de API. Este campo é opcional.
        /// </summary>
        public string? GoogleApiKey { get; set; }

        /// <summary>
        /// Token de acesso OAuth2. Este campo é opcional.
        /// </summary>
        public string? OAuth2AccessToken { get; set; }

        /// <summary>
        /// Refresh token OAuth2. Este campo é opcional.
        /// </summary>
        public string? OAuth2RefreshToken { get; set; }

        /// <summary>
        /// Data de expiração do token OAuth2. Este campo é opcional.
        /// </summary>
        public DateTime? OAuth2TokenExpiration { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        [JsonConstructor]
        public UpdateSectorRequestDTO(
            string? name = null,
            string? description = null,
            string? phoneNumberId = null,
            string? accessToken = null,
            bool? status = null,
            string? googleClientId = null,
            string? googleApiKey = null,
            string? oauth2AccessToken = null,
            string? oauth2RefreshToken = null,
            DateTime? oauth2TokenExpiration = null)
        {
            Name = name;
            Description = description;
            PhoneNumberId = phoneNumberId;
            AccessToken = accessToken;
            Status = status;
            GoogleClientId = googleClientId;
            GoogleApiKey = googleApiKey;
            OAuth2AccessToken = oauth2AccessToken;
            OAuth2RefreshToken = oauth2RefreshToken;
            OAuth2TokenExpiration = oauth2TokenExpiration;
        }
    }
}
