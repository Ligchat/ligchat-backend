using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("messages")]
    public class Messeageging
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("whatsapp_contact_id")]
        public int WhatsappContactId { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("profile_pic_url")]
        public string? ProfilePicUrl { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("message_type")]
        public string MessageType { get; set; }

        [Column("media_url")]
        public string? MediaUrl { get; set; }

        [Column("media_mimetype")]
        public string? MediaMimeType { get; set; }

        [Column("media_filename")]
        public string? MediaFilename { get; set; }

        [Column("is_sent")]
        public bool IsSent { get; set; }

        [Column("from")]
        public string From { get; set; }

        [Column("to")]
        public string To { get; set; }
    }
}
