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
        public string? MessageText { get; }

        /// <summary>
        /// Data e hora de envio da mensagem.
        /// </summary>
        public string? SendDate { get; }

        /// <summary>
        /// Identificador do fluxo associado à mensagem.
        /// </summary>
        public string? FlowId { get; }

        /// <summary>
        /// Identificador do setor ao qual a mensagem pertence.
        /// </summary>
        public int? SectorId { get; }

        /// <summary>
        /// Lista de identificadores de tags associadas à mensagem.
        /// </summary>
        public string TagIds { get; }

        /// <summary>
        /// Lista de anexos associados à mensagem.
        /// </summary>
        public List<AttachmentViewModel> Attachments { get; }

        /// <summary>
        /// Data e hora de criação da mensagem.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Data e hora da última atualização da mensagem.
        /// </summary>
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Nome da imagem associada à mensagem.
        /// </summary>
        public string? ImageName { get; }

        /// <summary>
        /// Nome do arquivo associado à mensagem.
        /// </summary>
        public string? FileName { get; }

        /// <summary>
        /// Anexo de imagem associado à mensagem.
        /// </summary>
        public string? ImageAttachment { get; }

        /// <summary>
        /// Anexo de arquivo associado à mensagem.
        /// </summary>
        public string? FileAttachment { get; }

        /// <summary>
        /// MimeType da imagem associada à mensagem.
        /// </summary>
        public string? ImageMimeType { get; }

        /// <summary>
        /// MimeType do arquivo associado à mensagem.
        /// </summary>
        public string? FileMimeType { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public MessageSchedulingViewModel()
        {
            Id = 0;
            Name = string.Empty;
            MessageText = string.Empty;
            SendDate = null;
            FlowId = null;
            SectorId = null;
            TagIds = string.Empty;
            Attachments = new List<AttachmentViewModel>();
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
            ImageName = null;
            FileName = null;
            ImageAttachment = null;
            FileAttachment = null;
            ImageMimeType = null;
            FileMimeType = null;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public MessageSchedulingViewModel(int id, string name, string? messageText, string? sendDate, string? flowId, int? sectorId, string tagIds, DateTime createdAt, DateTime updatedAt, string? imageName = null, string? fileName = null, string? imageAttachment = null, string? fileAttachment = null, string? imageMimeType = null, string? fileMimeType = null)
        {
            Id = id;
            Name = name;
            MessageText = messageText;
            SendDate = sendDate;
            FlowId = flowId;
            SectorId = sectorId;
            TagIds = tagIds;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ImageName = imageName;
            FileName = fileName;
            ImageAttachment = imageAttachment;
            FileAttachment = fileAttachment;
            ImageMimeType = imageMimeType;
            FileMimeType = fileMimeType;
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
