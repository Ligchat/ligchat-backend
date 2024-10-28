using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de ação do fluxo.
    /// </summary>
    public interface IFlowActionEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do fluxo.
        /// Representa o ID do fluxo ao qual esta ação pertence.
        /// </summary>
        int FlowId { get; set; }

        /// <summary>
        /// Tipo da ação.
        /// Define o tipo da ação a ser executada, podendo ser, por exemplo, "Notificação", "Atualização de status", etc.
        /// </summary>
        string ActionType { get; set; }

        /// <summary>
        /// Parâmetros da ação.
        /// Define os parâmetros necessários para a execução da ação. Podem incluir dados específicos requeridos pela ação.
        /// </summary>
        string ActionParameters { get; set; }
    }
}
