using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("whatsapp_accounts")] // Mapeia para a tabela whatsapp_accounts
    public class WhatsAppAccount
    {
        [Key] // Define a chave primária da tabela
        [Column("id")] // Mapeia para a coluna "id"
        public int Id { get; set; }

        [Required] // Define que o campo é obrigatório
        [Column("session_id")] // Mapeia para a coluna "session_id"
        public string SessionId { get; set; }  

        [Required] // Define que o campo é obrigatório
        [Column("is_connected")] // Mapeia para a coluna "is_connected"
        public bool IsConnected { get; set; } 

        [Column("qr_code")] // Mapeia para a coluna "qr_code"
        public string? QrCode { get; set; }  

        [Column("number")] // Mapeia para a coluna "number"
        public string? Number { get; set; }  
    }
}
