using System;

namespace LigChat.Backend.Domain.ViewModels
{
    public class MessageAttachmentViewModel
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string S3Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MessageAttachmentViewModel(
            int id,
            int messageId,
            string type,
            string fileName,
            string s3Url,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id;
            MessageId = messageId;
            Type = type;
            FileName = fileName;
            S3Url = s3Url;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
} 