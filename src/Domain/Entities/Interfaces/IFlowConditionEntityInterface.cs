using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de condição do fluxo.
    /// </summary>
    public interface IFlowConditionEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do fluxo.
        /// Representa o ID do fluxo ao qual esta condição pertence.
        /// </summary>
        int FlowId { get; set; }

        /// <summary>
        /// Nome da condição.
        /// Define o nome que descreve esta condição específica no fluxo.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Tipo da condição.
        /// Define o tipo da condição, podendo especificar o tipo de verificação ou critério aplicado.
        /// Exemplos: "Igualdade", "Maior que", "Menor que", etc.
        /// </summary>
        string ConditionType { get; set; }

        /// <summary>
        /// Valor da condição.
        /// Define o valor contra o qual a condição será avaliada.
        /// </summary>
        string ConditionValue { get; set; }
    }
}
