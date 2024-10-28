namespace LigChat.Backend.Application.Common.Mappings.FlowNodeResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do nó de fluxo na API.
    /// </summary>
    public class FlowNodeViewModel
    {
        // Identificador único do nó de fluxo
        public int Id { get; }

        // Identificador do fluxo associado
        public int FlowId { get; }

        // Título do nó
        public string Title { get; }

        // Descrição do nó
        public string? Description { get; }

        // Identificador da ação associada
        public int ActionId { get; }

        // Status do nó
        public bool Status { get; }

        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        // Data e hora de criação do nó
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização do nó
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowNodeViewModel()
        {
            Id = 0;
            FlowId = 0;
            Title = string.Empty;
            Description = string.Empty;
            ActionId = 0;
            Status = true;
            SectorId = null;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowNodeViewModel(int id, int flowId, string title, int actionId, string? description, bool status, int? sectorId)
        {
            Id = id;
            FlowId = flowId;
            Title = title;
            ActionId = actionId;
            Description = description;
            Status = status;
            SectorId = sectorId;
        }
    }
}
