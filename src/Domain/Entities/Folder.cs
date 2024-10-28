using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de pasta no sistema.
    /// Mapeia a tabela "pastas" no banco de dados e herda de BaseEntity.
    /// </summary>
    [Table("pastas")]
    public class Folder : IFolderEntityInterface
    {
        /// <summary>
        /// Identificador único da entidade.
        /// Deve ser um valor único para cada entidade e geralmente é gerado automaticamente.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome da pasta.
        /// Este campo é obrigatório e é utilizado para identificar a pasta.
        /// </summary>
        [Column("nome_pasta")]
        [Required]
        [StringLength(255, ErrorMessage = "O nome da pasta não pode exceder 255 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// Identificador do setor associado à pasta.
        /// Este campo é opcional e representa o ID do setor ao qual a pasta pode pertencer.
        /// </summary>
        [Column("sector_id")]
        public int? SectorId { get; set; }

        /// <summary>
        /// Data e hora de criação da pasta.
        /// Este campo é utilizado para registrar quando a pasta foi criada.
        /// </summary>
        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização da pasta.
        /// Este campo é utilizado para registrar quando a pasta foi atualizada pela última vez.
        /// </summary>
        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Status da entidade.
        /// Indica se a entidade está ativa ou inativa.
        /// </summary>
        [Column("status")]
        public bool Status { get; set; } = true;

        /// <summary>
        /// Construtor padrão da classe Folder.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public Folder()
        {
            Name = string.Empty;
            SectorId = null;
            Status = true; // Inicializa o Status da entidade base
            CreatedAt = UpdatedAt = DateTime.UtcNow; // Inicializa as datas da entidade base
        }

        /// <summary>
        /// Construtor da classe Folder que inicializa a pasta com um nome e setor específicos.
        /// </summary>
        /// <param name="name">Nome da pasta.</param>
        /// <param name="sectorId">Identificador do setor associado (opcional).</param>
        public Folder(string name, int? sectorId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo ou vazio.");

            Name = name;
            SectorId = sectorId;
            Status = true; // Inicializa o Status da entidade base
            CreatedAt = UpdatedAt = DateTime.UtcNow; // Inicializa as datas da entidade base
        }
    }
}
