using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade Flow.
    /// </summary>
    public interface IFlowEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do fluxo.
        /// Representa o nome dado ao fluxo, utilizado para identificá-lo e diferenciá-lo de outros fluxos.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Identificador da pasta.
        /// Representa o ID da pasta ou categoria à qual este fluxo pertence, ajudando na organização e estruturação dos fluxos.
        /// </summary>
        int FolderId { get; set; }
    }
}
