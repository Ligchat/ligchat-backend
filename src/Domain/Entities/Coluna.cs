using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tests_.src.Domain.Entities
{
    [Table("colunas")]
    public class Coluna
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("sector_id")]
        public int SectorId { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
