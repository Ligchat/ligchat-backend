using System;

namespace tests_.src.Domain.DTOs.CardDto
{
    public class UpdateCardRequestDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string? AssignedTo { get; set; }
        public string? AssigneeAvatar { get; set; }
        public DateTime? LastContact { get; set; }
        public string? Priority { get; set; }
        public ulong ContactId { get; set; }
        public int SectorId { get; set; }
        public int Position { get; set; }
        public int? TagId { get; set; }

        public UpdateCardRequestDTO()
        {
            Title = string.Empty;
            Content = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Status = "Novo";
        }
    }
} 