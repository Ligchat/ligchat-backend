using System.ComponentModel.DataAnnotations;

namespace LigChat.Backend.Domain.DTOs.MessageSchedulingDto
{
    public class AttachmentDTO
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Data { get; set; }

        public string? Base64Content { get; set; }

        public string? MimeType { get; set; }

        public AttachmentDTO()
        {
            Type = string.Empty;
            Name = string.Empty;
        }

        public AttachmentDTO(string type, string name, string? data = null, string? base64Content = null, string? mimeType = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Data = data;
            Base64Content = base64Content;
            MimeType = mimeType;
        }
    }
} 