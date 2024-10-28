using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    [Table("fluxos")]
    public class Flow : IFlowEntityInterface
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("nome_fluxo")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ID da pasta deve ser um valor positivo.")]
        [Column("pasta_fluxo")]
        public int FolderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do setor deve ser um valor positivo.")]
        [Column("id_setor")]
        public int SectorId { get; set; }

        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("executions")]
        public int Executions { get; set; } = 0;

        [Column("ctr")]
        public double Ctr { get; set; } = 0.0;

        public virtual Folder? Folder { get; set; }

        public Flow()
        {
            Name = string.Empty;
            FolderId = 0;
            SectorId = 0;
            Status = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Executions = 0;
            Ctr = 0.0;
            Folder = null;
        }

        public Flow(string name, int folderId, int sectorId, bool status = true, int executions = 0, double ctr = 0.0, DateTime? createdAt = null, DateTime? updatedAt = null, Folder? folder = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));

            if (folderId <= 0)
                throw new ArgumentOutOfRangeException(nameof(folderId), "O ID da pasta deve ser um valor positivo.");

            if (sectorId <= 0)
                throw new ArgumentOutOfRangeException(nameof(sectorId), "O ID do setor deve ser um valor positivo.");

            Name = name;
            FolderId = folderId;
            SectorId = sectorId;
            Status = status;
            Executions = executions;
            Ctr = ctr;
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
            Folder = folder;
        }
    }
}
