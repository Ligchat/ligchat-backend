namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da tag na API.
    /// </summary>
    public class TagViewModel
    {
        // Identificador único da tag
        public int Id { get; }

        // Nome da tag
        public string Name { get; }

        // Descrição da tag (opcional)
        public string? Description { get; }

        // Identificador do setor associado à tag (opcional)
        public int? SectorId { get; }

        // Data de criação da tag
        public DateTime CreatedAt { get; }

        // Data da última atualização da tag
        public DateTime UpdatedAt { get; }

        // Status da tag
        public bool Status { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public TagViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            SectorId = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Status = true;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public TagViewModel(int id, string name, string? description, int? sectorId, bool status)
        {
            Id = id;
            Name = name;
            Description = description;
            SectorId = sectorId;
            Status = status;
        }
    }
}
