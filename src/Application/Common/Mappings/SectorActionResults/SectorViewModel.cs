namespace LigChat.Backend.Application.Common.Mappings.SectorActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do setor na API.
    /// </summary>
    public class SectorViewModel
    {
        // Identificador único do setor
        public int Id { get; private set; }

        // Nome do setor
        public string Name { get; private set; }

        // Descrição do setor
        public string Description { get; private set; }

        // Identificador do negócio do usuário associado ao setor (opcional)
        public int? UserBusinessId { get; private set; }

        // Status da entidade (ativa ou inativa)
        public bool? Status { get; private set; }

        // Identificador do número de telefone associado ao setor
        public string? PhoneNumberId { get; private set; }

        // Token de acesso para o setor
        public string? AccessToken { get; private set; }

        // Data e hora de criação do setor
        public DateTime CreatedAt { get; private set; }

        // Data e hora da última atualização do setor
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public SectorViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            UserBusinessId = null;
            Status = true;
            PhoneNumberId = null;
            AccessToken = null;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="id">Identificador único do setor.</param>
        /// <param name="name">Nome do setor.</param>
        /// <param name="description">Descrição do setor.</param>
        /// <param name="userBusinessId">Identificador do negócio do usuário associado ao setor (opcional).</param>
        /// <param name="status">Status do setor (ativo ou inativo).</param>
        /// <param name="phoneNumberId">Identificador do número de telefone associado ao setor.</param>
        /// <param name="accessToken">Token de acesso para o setor.</param>
        /// <param name="createdAt">Data de criação do setor.</param>
        /// <param name="updatedAt">Data da última atualização do setor.</param>
        public SectorViewModel(
            int id,
            string name,
            string description,
            int? userBusinessId,
            bool? status,
            string? phoneNumberId,
            string? accessToken,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Description = description;
            UserBusinessId = userBusinessId;
            Status = status;
            PhoneNumberId = phoneNumberId;
            AccessToken = accessToken;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
