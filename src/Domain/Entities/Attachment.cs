namespace tests_.src.Domain.Entities
{
    public class Attachment
    {
        /// <summary>
        /// Identificador do anexo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo do anexo (por exemplo, imagem, documento).
        /// </summary>
        public string AttachmentType { get; set; }

        /// <summary>
        /// Conteúdo do anexo em base64.
        /// </summary>
        public string AttachmentContent { get; set; }

        /// <summary>
        /// Identificador da mensagem agendada associada a este anexo.
        /// </summary>
        public int MessageSchedulingId { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Attachment()
        {
            AttachmentType = string.Empty;
            AttachmentContent = string.Empty;
        }

        /// <summary>
        /// Construtor para inicializar um anexo.
        /// </summary>
        /// <param name="attachmentType">Tipo do anexo.</param>
        /// <param name="attachmentContent">Conteúdo do anexo.</param>
        /// <param name="messageSchedulingId">ID da mensagem agendada associada.</param>
        public Attachment(string attachmentType, string attachmentContent, int messageSchedulingId)
        {
            AttachmentType = attachmentType ?? throw new ArgumentNullException(nameof(attachmentType), "O tipo do anexo não pode ser nulo.");
            AttachmentContent = attachmentContent ?? throw new ArgumentNullException(nameof(attachmentContent), "O conteúdo do anexo não pode ser nulo.");
            MessageSchedulingId = messageSchedulingId;
        }
    }
}
