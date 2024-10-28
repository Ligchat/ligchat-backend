namespace LigChat.Backend.Application.Common.Mappings.FlowActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da ação do fluxo na API.
    /// </summary>
    public class FlowActionViewModel
    {
        // Identificador único da ação do fluxo
        public int Id { get; }
        
        // Identificador do fluxo associado
        public int FlowId { get; }
        
        // Tipo da ação
        public string ActionType { get; }
        
        // Parâmetros da ação
        public string ActionParameters { get; }
        
        // Status da ação
        public bool Status { get; }
        
        // Identificador do setor associado ao fluxo (opcional)
        public int? SectorId { get; }
        
        // Data e hora de criação da ação
        public DateTime CreatedAt { get; }
        
        // Data e hora da última atualização da ação
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowActionViewModel()
        {
            Id = 0;
            FlowId = 0;
            ActionType = string.Empty;
            ActionParameters = string.Empty;
            Status = false;
            SectorId = null;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowActionViewModel(int id, int flowId, string actionType, string actionParameters, bool status, int? sectorId = null)
        {
            Id = id;
            FlowId = flowId;
            ActionType = actionType;
            ActionParameters = actionParameters;
            Status = status;
            SectorId = sectorId;
        }
    }
}
