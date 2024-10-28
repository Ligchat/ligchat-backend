namespace LigChat.Backend.Application.Common.Mappings.TeamActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da equipe na API.
    /// </summary>
    public class TeamViewModel
    {
        // Identificador único da equipe
        public int Id { get; }

        // Nome da equipe
        public string Name { get; }

        // Identificador do setor associado à equipe (opcional)
        public int? SectorId { get; }

        // Permissões associadas à equipe
        public string[] Permissions { get; }

        // Data de criação da equipe
        public DateTime CreatedAt { get; }

        // Data da última atualização da equipe
        public DateTime UpdatedAt { get; }

        // Status da equipe
        public bool Status { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public TeamViewModel()
        {
            Id = 0;
            Name = string.Empty;
            SectorId = null;
            Permissions = Array.Empty<string>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Status = true;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public TeamViewModel(int id, string name, int? sectorId, string[] permissions, bool status)
        {
            Id = id;
            Name = name;
            SectorId = sectorId;
            Permissions = permissions ?? Array.Empty<string>();
            Status = status;
        }
    }
}
