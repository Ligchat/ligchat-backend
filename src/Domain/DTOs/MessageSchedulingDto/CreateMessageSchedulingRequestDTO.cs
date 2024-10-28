using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.MessageSchedulingDto
{
    /// <summary>
    /// DTO para a criação de uma nova mensagem agendada.
    /// </summary>
    public class CreateMessageSchedulingRequestDTO
    {
        /// <summary>
        /// Nome da mensagem agendada. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Texto da mensagem. Este campo é opcional.
        /// </summary>
        public string? MessageText { get; set; }

        /// <summary>
        /// Data de envio da mensagem. Este campo é obrigatório.
        /// </summary>
        public string SendDate { get; set; }

        /// <summary>
        /// Identificador do fluxo ao qual a mensagem está associada. Opcional.
        /// </summary>
        public string? FlowId { get; set; }

        /// <summary>
        /// Identificador do setor ao qual a mensagem pertence. Opcional.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Lista de identificadores de tags associadas à mensagem.
        /// </summary>
        public string TagIds { get; set; }

        /// <summary>
        /// Lista de anexos associados à mensagem.
        /// </summary>
        public List<AttachmentDTO> Attachments { get; set; } = new List<AttachmentDTO>();

        /// <summary>
        /// Nome da imagem associada à mensagem. Opcional.
        /// </summary>
        public string? ImageName { get; set; }

        /// <summary>
        /// Nome do arquivo associado à mensagem. Opcional.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Anexo de imagem associado à mensagem. Opcional.
        /// </summary>
        public string? ImageAttachment { get; set; }

        /// <summary>
        /// Anexo de arquivo associado à mensagem. Opcional.
        /// </summary>
        public string? FileAttachment { get; set; }

        /// <summary>
        /// MimeType da imagem associada à mensagem. Opcional.
        /// </summary>
        public string? ImageMimeType { get; set; }

        /// <summary>
        /// MimeType do arquivo associado à mensagem. Opcional.
        /// </summary>
        public string? FileMimeType { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome da mensagem agendada.</param>
        /// <param name="messageText">Texto da mensagem.</param>
        /// <param name="sendDate">Data de envio da mensagem.</param>
        /// <param name="flowId">Identificador do fluxo.</param>
        /// <param name="sectorId">Identificador do setor.</param>
        /// <param name="tagIds">Lista de IDs de tags associadas.</param>
        /// <param name="attachments">Lista de anexos.</param>
        /// <param name="imageName">Nome da imagem associada.</param>
        /// <param name="fileName">Nome do arquivo associado.</param>
        /// <param name="imageAttachment">Anexo de imagem.</param>
        /// <param name="fileAttachment">Anexo de arquivo.</param>
        /// <param name="imageMimeType">MimeType da imagem.</param>
        /// <param name="fileMimeType">MimeType do arquivo.</param>
        [JsonConstructor]
        public CreateMessageSchedulingRequestDTO(
            string name,
            string? messageText,
            string sendDate,
            string? flowId = null,
            int? sectorId = null,
            string tagIds = "",
            List<AttachmentDTO>? attachments = null,
            string? imageName = null,
            string? fileName = null,
            string? imageAttachment = null,
            string? fileAttachment = null,
            string? imageMimeType = null,
            string? fileMimeType = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "O nome da mensagem não pode ser nulo.");
            MessageText = messageText;
            SendDate = sendDate;
            FlowId = flowId;
            SectorId = sectorId;
            TagIds = tagIds;
            Attachments = attachments ?? new List<AttachmentDTO>();
            ImageName = imageName;
            FileName = fileName;
            ImageAttachment = imageAttachment;
            FileAttachment = fileAttachment;
            ImageMimeType = imageMimeType;
            FileMimeType = fileMimeType;
        }
    }
}
