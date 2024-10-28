using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade FlowTransition, que representa uma transição entre nós em um fluxo.
    /// </summary>
    public interface IFlowTransitionEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do fluxo.
        /// Representa o ID do fluxo ao qual esta transição pertence.
        /// </summary>
        int FlowId { get; set; }

        /// <summary>
        /// Identificador do nó de origem.
        /// Representa o ID do nó de origem de onde a transição inicia.
        /// </summary>
        int FromNodeId { get; set; }

        /// <summary>
        /// Identificador do nó de destino.
        /// Representa o ID do nó de destino para onde a transição leva.
        /// </summary>
        int ToNodeId { get; set; }

        /// <summary>
        /// Identificador da condição.
        /// Representa o ID da condição que deve ser atendida para que a transição ocorra.
        /// </summary>
        int ConditionId { get; set; }
    }
}
