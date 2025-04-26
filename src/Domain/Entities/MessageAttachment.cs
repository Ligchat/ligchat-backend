using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigChat.Backend.Domain.Entities
{
    [Table("mensagens_anexos")]
    public class MessageAttachment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("mensagem_id")]
        public int MessageId { get; set; }

        [Column("tipo")]
        [Required]
        [StringLength(10)]
        public string Type { get; set; }

        [Column("nome_arquivo")]
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Column("url_s3")]
        [Required]
        [StringLength(500)]
        public string S3Url { get; set; }

        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("MessageId")]
        public MessageScheduling Message { get; set; }

        public MessageAttachment()
        {
            Type = string.Empty;
            FileName = string.Empty;
            S3Url = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public MessageAttachment(string type, string fileName, string s3Url)
        {
            Type = type;
            FileName = fileName;
            S3Url = s3Url;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 