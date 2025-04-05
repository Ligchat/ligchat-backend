using System;

namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? SectorId { get; set; }
        public string Color { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TagViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            SectorId = null;
            Color = "#000000";
            Status = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public TagViewModel(
            int id,
            string name,
            string? description,
            int? sectorId,
            string color,
            bool status,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            Id = id;
            Name = name;
            Description = description;
            SectorId = sectorId;
            Color = color;
            Status = status;
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
        }
    }
}