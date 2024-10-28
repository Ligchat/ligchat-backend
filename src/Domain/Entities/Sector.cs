using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces; // Namespace da interface

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade Sector no sistema.
    /// Mapeia a tabela "setores" no banco de dados e herda de BaseEntity.
    /// </summary>
    [Table("setores")]
    public class Sector : ISectorEntityInterface
    {
        /// <summary>
        /// Identificador único da entidade.
        /// Deve ser um valor único para cada entidade e geralmente é gerado automaticamente.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do setor.
        /// Este campo é obrigatório e representa o nome pelo qual o setor é conhecido.
        /// </summary>
        [Column("nome_setor")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Descrição do setor.
        /// Utilizado para fornecer detalhes adicionais sobre o setor.
        /// Este campo é obrigatório.
        /// </summary>
        [Column("descricao_setor")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Identificador do negócio do usuário associado ao setor.
        /// Este campo é opcional e pode ser utilizado para associar o setor a um negócio específico do usuário.
        /// </summary>
        [Column("id_negocio_usuario")]
        public int? UserBusinessId { get; set; }

        [Column("numero")] // Define o nome da coluna em snake_case
        public string PhoneNumberId { get; set; }

        [Column("token_acesso")] // Define o nome da coluna em snake_case
        public string AccessToken { get; set; }

        /// <summary>
        /// Data e hora de criação do setor.
        /// Este campo é utilizado para registrar quando o setor foi criado.
        /// </summary>
        [Column("data_criacao")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização do setor.
        /// Este campo é utilizado para registrar quando o setor foi atualizado pela última vez.
        /// </summary>
        [Column("data_atualizacao")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Status da entidade.
        /// Indica se a entidade está ativa ou inativa.
        /// </summary>
        [Column("status")]
        public bool? Status { get; set; } = true;

        /// <summary>
        /// Construtor padrão da classe Sector.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public Sector()
        {
            Name = string.Empty;
            Description = string.Empty;
            PhoneNumberId = string.Empty;
            AccessToken = string.Empty;
            UserBusinessId = null;
            Status = true;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Construtor da classe Sector que inicializa o setor com valores específicos.
        /// </summary>
        /// <param name="name">Nome do setor.</param>
        /// <param name="description">Descrição do setor.</param>
        /// <param name="userBusinessId">Identificador do negócio do usuário associado ao setor.</param>
        /// <param name="phoneNumberId">Número de telefone associado ao setor.</param>
        /// <param name="accessToken">Token de acesso para o setor.</param>
        public Sector(string name, string description, int? userBusinessId, string phoneNumberId, string accessToken)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            UserBusinessId = userBusinessId;
            PhoneNumberId = phoneNumberId ?? throw new ArgumentNullException(nameof(phoneNumberId));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            Status = true;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
    }
}
