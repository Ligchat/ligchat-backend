﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigChat.Backend.Domain.Entities
{
    [Table("mensagens_agendadas")]
    public class MessageScheduling
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required]
        public string Name { get; set; }

        [Column("mensagem_de_texto")]
        [Required]
        public string MessageText { get; set; }

        [Column("data_envio")]
        [Required]
        public string SendDate { get; set; }

        [Column("contato_id")]
        [Required]
        public int ContactId { get; set; }

        [Column("setor_id")]
        [Required]
        public int SectorId { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("tag_id")]
        public string TagIds { get; set; }

        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        public MessageScheduling()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Name = string.Empty;
            MessageText = string.Empty;
            SendDate = string.Empty;
            TagIds = string.Empty;
        }

        public MessageScheduling(
            string name,
            string messageText,
            string sendDate,
            int contactId,
            int sectorId,
            bool status = true,
            string tagIds = "")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
            SendDate = sendDate ?? throw new ArgumentNullException(nameof(sendDate));
            ContactId = contactId;
            SectorId = sectorId;
            Status = status;
            TagIds = tagIds;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
