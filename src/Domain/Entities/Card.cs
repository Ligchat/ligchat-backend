using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LigChat.Backend.Domain.Entities;

namespace tests_.src.Domain.Entities
{
    [Table("cards", Schema = "ligchat")]
    public class Card
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("contact_id")]
        public int? ContactId { get; set; }

        [Column("column_id")]
        public int? ColumnId { get; set; }

        [Column("position")]
        [Required]
        public int Position { get; set; } = 1;

        [Column("sector_id")]
        public int? SectorId { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ContactId")]
        public Contact? Contact { get; set; }

        [ForeignKey("ColumnId")]
        [JsonIgnore]
        public Coluna? Column { get; set; }

        public Card()
        {
            CreatedAt = DateTime.UtcNow;
            Position = 1;
        }
    }
}
