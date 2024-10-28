using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tests_.src.Domain.Entities
{
    [Table("contatos")]
    public class Contato
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("message_id")]
        public int? MessageId { get; set; }

        [Column("attachment_id")]
        public int? AttachmentId { get; set; }

        [Column("general_info")]
        public string GeneralInfo { get; set; }
    }
}
