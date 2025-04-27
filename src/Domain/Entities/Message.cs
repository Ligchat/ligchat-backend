using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("messages")]
    public class Messeageging
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("conteudo")]
        public string Conteudo { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("nome_arquivo")]
        public string NomeArquivo { get; set; }

        [Column("mime_type")]
        public string MimeType { get; set; }

        [Column("id_setor")]
        public int IdSetor { get; set; }

        [Column("contato_id")]
        public int ContatoId { get; set; }

        [Column("data_envio")]
        public DateTime? DataEnvio { get; set; }

        [Column("enviado")]
        public bool Enviado { get; set; }

        [Column("lido")]
        public bool Lido { get; set; }

        [Column("WhatsAppMessageId")]
        public string WhatsAppMessageId { get; set; }

        [Column("is_official")]
        public bool? IsOfficial { get; set; }
    }
}
