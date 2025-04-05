using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("colunas", Schema = "ligchat")]
    public class Coluna
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("sector_id")]
        [Required]
        public int SectorId { get; set; }

        [Column("position")]
        [Required]
        public int Position { get; set; }

        [InverseProperty("Column")]
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
