using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigChat.Backend.Domain.Entities
{
    [Table("agents")]
    public class Agent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("api_key")]
        public string ApiKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("model")]
        public string Model { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("prompt")]
        public string? Prompt { get; set; }

        [Required]
        [Column("sector_id")]
        public int SectorId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SectorId")]
        public virtual Sector? Sector { get; set; }
    }
} 