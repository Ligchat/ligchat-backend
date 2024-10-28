using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade FlowNode, que representa um nó em um fluxo.
    /// </summary>
    public interface IFlowNodeEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do fluxo.
        /// Representa o ID do fluxo ao qual este nó pertence.
        /// </summary>
        int FlowId { get; set; }

        /// <summary>
        /// Título do nó.
        /// Define o título ou nome do nó, que é utilizado para identificá-lo dentro do fluxo.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Descrição do nó.
        /// Opcional: pode incluir detalhes adicionais ou informações contextuais sobre o nó.
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Identificador da ação.
        /// Representa o ID da ação associada a este nó, indicando qual ação será executada quando o nó for processado.
        /// </summary>
        int ActionId { get; set; }
    }
}
