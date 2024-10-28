namespace LigChat.Backend.Application.Common.Mappings.FlowTransitionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da transição de fluxo na API.
    /// </summary>
    public class FlowTransitionViewModel
    {
        // Identificador único da transição de fluxo
        public int Id { get; }

        // Identificador do fluxo associado
        public int FlowId { get; }

        // Identificador do nó de origem
        public int FromNodeId { get; }

        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        // Identificador do nó de destino
        public int ToNodeId { get; }

        // Identificador da condição associada
        public int ConditionId { get; }

        // Status da transição de fluxo
        public bool Status { get; }

        // Data e hora de criação da transição de fluxo
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização da transição de fluxo
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowTransitionViewModel()
        {
            Id = 0;
            FlowId = 0;
            FromNodeId = 0;
            SectorId = null;
            ToNodeId = 0;
            ConditionId = 0;
            Status = false;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowTransitionViewModel(int id, int flowId, int fromNodeId, int? sectorId, int toNodeId, int conditionId, bool status)
        {
            Id = id;
            FlowId = flowId;
            FromNodeId = fromNodeId;
            SectorId = sectorId;
            ToNodeId = toNodeId;
            ConditionId = conditionId;
            Status = status;
        }
    }
}
