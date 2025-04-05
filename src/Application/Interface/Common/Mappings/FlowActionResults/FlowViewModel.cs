using LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Application.Common.Mappings.FlowResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do fluxo na API.
    /// </summary>
    public class FlowViewModel
    {
        // Identificador único do fluxo
        public string Id { get; }

        // Nome do fluxo
        public string Name { get; }

        // Pasta associada ao fluxo
        public int FolderId { get; }

        // Identificador do setor associado ao fluxo (opcional)
        public int? SectorId { get; }

        // Data e hora de criação do fluxo
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização do fluxo
        public DateTime UpdatedAt { get; }

        public Folder? Folder { get; }

        public Sector? Sector { get; }

        // Status do fluxo
        public bool Status { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowViewModel()
        {
            Id = "0";
            Name = string.Empty;
            FolderId = 0;
            SectorId = null;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
            Status = true;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowViewModel(string id, string name, int folderId, int? sectorId, bool status, Folder? folder = null, Sector? sector = null)
        {
            Id = id;
            Name = name;
            FolderId = folderId;
            SectorId = sectorId;
            Status = status;
            Folder = folder;
            Sector = sector;
        }
    }
}
