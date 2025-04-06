namespace LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados de mensagens agendadas na API.
    /// </summary>
    public class MessageSchedulingViewModel
    {
        /// <summary>
        /// Identificador único da mensagem agendada.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Nome da mensagem agendada.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Texto da mensagem.
        /// </summary>
        public string MessageText { get; }

        /// <summary>
        /// Data e hora de envio da mensagem.
        /// </summary>
        public string SendDate { get; }

        /// <summary>
        /// Identificador do contato associado à mensagem.
        /// </summary>
        public int ContactId { get; }

        /// <summary>
        /// Identificador do setor ao qual a mensagem pertence.
        /// </summary>
        public int SectorId { get; }

        /// <summary>
        /// Status da mensagem agendada.
        /// </summary>
        public bool Status { get; }

        /// <summary>
        /// Lista de identificadores de tags associadas à mensagem.
        /// </summary>
        public string TagIds { get; }

        /// <summary>
        /// Data e hora de criação da mensagem.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Data e hora da última atualização da mensagem.
        /// </summary>
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public MessageSchedulingViewModel(
            int id,
            string name,
            string messageText,
            string sendDate,
            int contactId,
            int sectorId,
            bool status,
            string tagIds,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id;
            Name = name;
            MessageText = messageText;
            SendDate = sendDate;
            ContactId = contactId;
            SectorId = sectorId;
            Status = status;
            TagIds = tagIds;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }

    /// <summary>
    /// Representa um modelo de visualização para anexos de mensagens agendadas.
    /// </summary>
    public class AttachmentViewModel
    {
        /// <summary>
        /// Tipo do anexo (por exemplo, imagem, documento).
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Conteúdo do anexo em base64.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Construtor para inicializar o DTO do anexo.
        /// </summary>
        /// <param name="type">Tipo do anexo.</param>
        /// <param name="content">Conteúdo do anexo.</param>
        public AttachmentViewModel(string type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
