namespace tests_.src.Domain.Entities
{
    using global::LigChat.Backend.Domain.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace LigChat.Backend.Domain.Entities
    {
        /// <summary>
        /// Representa o relacionamento entre usuários e setores.
        /// </summary>
        [Table("user_sector")]
        public class UserSector
        {
            /// <summary>
            /// Identificador único do relacionamento.
            /// </summary>
            [Key]
            [Column("id")]
            public int Id { get; set; }

            /// <summary>
            /// Identificador do usuário associado.
            /// </summary>
            [Column("user_id")]
            public int UserId { get; set; }

            /// <summary>
            /// Identificador do setor associado.
            /// </summary>
            [Column("sector_id")]
            public int SectorId { get; set; }

            /// <summary>
            /// Indica se o setor é compartilhado.
            /// </summary>
            [Column("is_shared")]
            public bool IsShared { get; set; } = false;

            /// <summary>
            /// Referência para a entidade Sector.
            /// </summary>
            public Sector Sector { get; set; }

            /// <summary>
            /// Construtor padrão da classe UserSector.
            /// </summary>
            public UserSector() { }

            /// <summary>
            /// Construtor para inicializar o UserSector com valores específicos.
            /// </summary>
            public UserSector(int userId, int sectorId, bool isShared = false)
            {
                UserId = userId;
                SectorId = sectorId;
                IsShared = isShared;
            }
        }
    }

}
