namespace LigChat.Backend.Application.Common.Mappings.FlowConditionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados da condição do fluxo na API.
    /// </summary>
    public class FlowConditionViewModel
    {
        // Identificador único da condição do fluxo
        public int Id { get; }

        // Identificador do fluxo associado
        public int FlowId { get; }

        // Nome da condição
        public string Name { get; }

        // Tipo da condição
        public string ConditionType { get; }

        // Valor da condição
        public string ConditionValue { get; }

        // Status da condição
        public bool Status { get; }

        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        // Data e hora de criação da condição
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização da condição
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowConditionViewModel()
        {
            Id = 0;
            FlowId = 0;
            Name = string.Empty;
            ConditionType = string.Empty;
            ConditionValue = string.Empty;
            Status = true;
            SectorId = null;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowConditionViewModel(int id, int flowId, string name, string conditionType, string conditionValue, int? sectorId, bool status)
        {
            Id = id;
            FlowId = flowId;
            Name = name;
            ConditionType = conditionType;
            ConditionValue = conditionValue;
            Status = status;
            SectorId = sectorId;
        }
    }
}
