namespace LigChat.Backend.Application.Common.Mappings.FolderResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da pasta na API.
    /// </summary>
    public class FolderViewModel
    {
        // Identificador único da pasta
        public int Id { get; }

        // Nome da pasta
        public string Name { get; }

        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        // Status da pasta
        public bool Status { get; }

        // Data e hora de criação da pasta
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização da pasta
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FolderViewModel()
        {
            Id = 0;
            Name = string.Empty;
            SectorId = null;
            Status = true;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FolderViewModel(int id, string name, int? sectorId, bool status)
        {
            Id = id;
            Name = name;
            SectorId = sectorId;
            Status = status;
        }
    }
}
