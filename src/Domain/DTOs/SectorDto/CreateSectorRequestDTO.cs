using System;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.SectorDto
{
    /// <summary>
    /// DTO para a criação de um novo setor.
    /// </summary>
    public class CreateSectorRequestDTO
    {
        /// <summary>
        /// Nome do setor. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do setor. Este campo é obrigatório.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador do usuário responsável pelo setor. Este campo é obrigatório.
        /// </summary>
        public int UserBusinessId { get; set; }

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
        /// Indica se o setor é compartilhado. Este campo é opcional e o padrão é false.
        /// </summary>
        public bool IsShared { get; set; } = false;

        /// <summary>
        /// Identificador original opcional do setor.
        /// </summary>

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do setor.</param>
        /// <param name="description">Descrição do setor.</param>
        /// <param name="userBusinessId">Identificador do usuário responsável pelo setor.</param>
        /// <param name="phoneNumberId">Número de telefone associado ao setor.</param>
        /// <param name="accessToken">Token de acesso para o setor.</param>
        /// <param name="status">Status do setor.</param>
        /// <param name="googleClientId">Client ID do Google para integrações de API.</param>
        /// <param name="googleApiKey">API Key do Google para integrações de API.</param>
        /// <param name="oauth2AccessToken">Token de acesso OAuth2.</param>
        /// <param name="oauth2RefreshToken">Refresh token OAuth2.</param>
        /// <param name="oauth2TokenExpiration">Data de expiração do token OAuth2.</param>
        /// <param name="isShared">Indica se o setor é compartilhado.</param>
        [JsonConstructor]
        public CreateSectorRequestDTO(
            string name,
            string description,
            int userBusinessId,
            string? phoneNumberId = null,
            string? accessToken = null,
            bool? status = null,
            string? googleClientId = null,
            string? googleApiKey = null,
            string? oauth2AccessToken = null,
            string? oauth2RefreshToken = null,
            DateTime? oauth2TokenExpiration = null,
            bool isShared = false)
        {
            Name = name;
            Description = description;
            UserBusinessId = userBusinessId;
            PhoneNumberId = phoneNumberId;
            AccessToken = accessToken;
            Status = status;
            GoogleClientId = googleClientId;
            GoogleApiKey = googleApiKey;
            OAuth2AccessToken = oauth2AccessToken;
            OAuth2RefreshToken = oauth2RefreshToken;
            OAuth2TokenExpiration = oauth2TokenExpiration;
            IsShared = isShared;
        }
    }
}
