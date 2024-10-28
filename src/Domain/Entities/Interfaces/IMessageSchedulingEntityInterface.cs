using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de mensagem agendada.
    /// Representa uma mensagem que está programada para ser enviada em um momento específico.
    /// </summary>
    public interface IMessageSchedulingEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Texto da mensagem.
        /// Contém o conteúdo textual da mensagem que será enviada.
        /// </summary>
        string MessageText { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada à mensagem.
        /// Referencia a tag usada para categorizar a mensagem.
        /// </summary>
        int? TagId { get; set; }

        /// <summary>
        /// Identificador do usuário responsável pela mensagem.
        /// Referencia o ID do usuário que criou ou é responsável pela mensagem.
        /// </summary>
        int? UserId { get; set; }

        /// <summary>
        /// Identificador do fluxo associado à mensagem.
        /// Representa o ID do fluxo ao qual a mensagem pertence.
        /// </summary>
        int? FlowId { get; set; }

        /// <summary>
        /// Data e hora programadas para o envio da mensagem.
        /// Define o momento exato em que a mensagem está agendada para ser enviada.
        /// </summary>
        DateTime? SendDate { get; set; }


    }
}
