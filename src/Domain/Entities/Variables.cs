using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("variables")]
    public class Variables
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("value")]
        [MaxLength(255)]
        public string Value { get; set; }

        [Column("sector_id")]
        public int? SectorId { get; set; }
    }
}