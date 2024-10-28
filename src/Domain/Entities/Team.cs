using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de equipe no sistema.
    /// Mapeia a tabela "times" no banco de dados.
    /// </summary>
    [Table("times")]
    public class Team : ITeamEntityInterface
    {
        /// <summary>
        /// Identificador único da entidade.
        /// Deve ser um valor único para cada entidade e geralmente é gerado automaticamente.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome da equipe.
        /// Este campo é obrigatório e representa o nome pelo qual a equipe é conhecida.
        /// </summary>
        [Column("nome")]
        [Required]
        [MaxLength(100)] // Limita o comprimento do nome a 100 caracteres
        public string Name { get; set; }

        /// <summary>
        /// Identificador do setor associado à equipe.
        /// Este campo é opcional e representa o ID do setor ao qual a equipe pertence.
        /// </summary>
        [Column("id_setor")]
        public int? SectorId { get; set; }

        /// <summary>
        /// Permissões associadas à equipe.
        /// Este campo armazena uma lista de permissões que a equipe possui.
        /// </summary>
        [Column("permissoes")]
        public string[] Permissions { get; set; } = Array.Empty<string>(); // Inicializa com um array vazio

        /// <summary>
        /// Data e hora de criação da equipe.
        /// Este campo é utilizado para registrar quando a equipe foi criada.
        /// </summary>
        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização da equipe.
        /// Este campo é utilizado para registrar quando a equipe foi atualizada pela última vez.
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
        /// Construtor padrão da classe Team.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public Team()
        {
            Name = string.Empty;
            SectorId = null;
            Permissions = Array.Empty<string>();
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Construtor da classe Team que inicializa a entidade com valores específicos.
        /// </summary>
        /// <param name="name">Nome da equipe.</param>
        /// <param name="sectorId">Identificador do setor associado (opcional).</param>
        /// <param name="status">Status da equipe (opcional).</param>
        /// <param name="permissions">Lista de permissões da equipe.</param>
        public Team(string name, int? sectorId = null, bool status = true, string[] permissions = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo ou vazio.");
            SectorId = sectorId;
            Status = status;
            Permissions = permissions ?? Array.Empty<string>();
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
    }
}
