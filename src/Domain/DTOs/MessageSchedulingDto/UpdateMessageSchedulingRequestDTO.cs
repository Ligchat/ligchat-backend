using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.MessageSchedulingDto
{
    /// <summary>
    /// DTO para a atualização das informações de uma mensagem agendada.
    /// </summary>
    public class UpdateMessageSchedulingRequestDTO
    {
        /// <summary>
        /// Nome da mensagem agendada. Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Texto da mensagem. Este campo é opcional.
        /// </summary>
        public string? MessageText { get; set; }

        /// <summary>
        /// Data de envio da mensagem. Este campo é opcional.
        /// </summary>
        public string? SendDate { get; set; }

        /// <summary>
        /// Identificador do contato ao qual a mensagem está associada. Este campo é opcional.
        /// </summary>
        public int? ContactId { get; set; }

        /// <summary>
        /// Identificador do setor ao qual a mensagem pertence. Este campo é opcional.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Status da mensagem. Este campo é opcional.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// Lista de identificadores de tags associadas à mensagem. Este campo é opcional.
        /// </summary>
        public string? TagIds { get; set; }

        /// <summary>
        /// Lista de anexos associados à mensagem. Este campo é opcional.
        /// </summary>
        public List<AttachmentDTO>? Attachments { get; set; }

        /// <summary>
        /// Nome da imagem associada à mensagem. Este campo é opcional.
        /// </summary>
        public string? ImageName { get; set; }

        /// <summary>
        /// Nome do arquivo associado à mensagem. Este campo é opcional.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Anexo de imagem associado à mensagem. Este campo é opcional.
        /// </summary>
        public string? ImageAttachment { get; set; }

        /// <summary>
        /// Anexo de arquivo associado à mensagem. Este campo é opcional.
        /// </summary>
        public string? FileAttachment { get; set; }

        /// <summary>
        /// MimeType da imagem associada à mensagem. Este campo é opcional.
        /// </summary>
        public string? ImageMimeType { get; set; }

        /// <summary>
        /// MimeType do arquivo associado à mensagem. Este campo é opcional.
        /// </summary>
        public string? FileMimeType { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome da mensagem agendada.</param>
        /// <param name="messageText">Texto da mensagem.</param>
        /// <param name="sendDate">Data de envio da mensagem.</param>
        /// <param name="contactId">Identificador do contato.</param>
        /// <param name="sectorId">Identificador do setor.</param>
        /// <param name="status">Status da mensagem.</param>
        /// <param name="tagIds">Lista de IDs de tags associadas.</param>
        /// <param name="attachments">Lista de anexos.</param>
        /// <param name="imageName">Nome da imagem associada.</param>
        /// <param name="fileName">Nome do arquivo associado.</param>
        /// <param name="imageAttachment">Anexo de imagem.</param>
        /// <param name="fileAttachment">Anexo de arquivo.</param>
        /// <param name="imageMimeType">MimeType da imagem.</param>
        /// <param name="fileMimeType">MimeType do arquivo.</param>
        [JsonConstructor]
        public UpdateMessageSchedulingRequestDTO(
            string? name = null,
            string? messageText = null,
            string? sendDate = null,
            int? contactId = null,
            int? sectorId = null,
            bool? status = null,
            string? tagIds = null,
            List<AttachmentDTO>? attachments = null,
            string? imageName = null,
            string? fileName = null,
            string? imageAttachment = null,
            string? fileAttachment = null,
            string? imageMimeType = null,
            string? fileMimeType = null)
        {
            Name = name;
            MessageText = messageText;
            SendDate = sendDate;
            ContactId = contactId;
            SectorId = sectorId;
            Status = status;
            TagIds = tagIds;
            Attachments = attachments;
            ImageName = imageName;
            FileName = fileName;
            ImageAttachment = imageAttachment;
            FileAttachment = fileAttachment;
            ImageMimeType = imageMimeType;
            FileMimeType = fileMimeType;
        }
    }
}
