using System.ComponentModel.DataAnnotations;

public class WhatsAppTextMessageDTO
{
    [Required]
    public string RecipientPhone { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public int SectorId { get; set; }
} 