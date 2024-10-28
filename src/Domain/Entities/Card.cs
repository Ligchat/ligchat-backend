using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tests_.src.Domain.Entities
{
    [Table("cards")]
    public class Card
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("contact_id")]
        public int ContactId { get; set; }

        [Column("tag_id")]
        public int? TagId { get; set; }

        [Column("column_id")]
        public int ColumnId { get; set; }

        [Column("last_contact")]
        public DateTime? LastContact { get; set; }

        // Relacionamento com a entidade Contato (opcional)
        [ForeignKey("ContactId")]
        public virtual Contato? Contato { get; set; }

        // Relacionamento com a entidade Coluna (opcional)
        [ForeignKey("ColumnId")]
        public virtual Coluna? Column { get; set; }
    }
}
