using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade FlowShared, que representa um fluxo compartilhado.
    /// </summary>
    public interface IFlowSharedEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do fluxo.
        /// Representa o ID do fluxo ao qual este fluxo compartilhado está associado.
        /// </summary>
        int FlowId { get; set; }

        /// <summary>
        /// Link do fluxo compartilhado.
        /// Contém o link ou URL relacionado ao fluxo compartilhado, que pode ser utilizado para acessar ou visualizar o fluxo.
        /// </summary>
        string Link { get; set; }
    }
}
