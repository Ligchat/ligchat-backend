using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de contato no sistema.
    /// Mapeia a tabela "contacts" no banco de dados e herda de BaseEntity.
    /// </summary>
    [Table("contacts", Schema = "ligchat")]
    public class Contact : IContactEntityInterface
    {
        /// <summary>
        /// Identificador único do contato.
        /// Este campo é a chave primária da tabela.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do contato.
        /// Representa o nome completo ou o nome pelo qual o contato é conhecido.
        /// </summary>
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Número de WhatsApp do contato.
        /// Utilizado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        [Required]
        [Column("number")]
        [StringLength(15)]
        public string Number { get; set; }

        /// <summary>
        /// URL da foto de perfil do WhatsApp (opcional).
        /// </summary>
        [Column("avatar_url")]
        [StringLength(500)]
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Identificador do setor associado ao usuário.
        /// Este campo é obrigatório e representa o ID do setor ao qual o usuário pertence.
        /// </summary>
        [Required]
        [Column("sector_id")]
        public int SectorId { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada ao contato.
        /// Referencia a tag que categoriza o contato.
        /// </summary>
        [Column("tag_id")]
        public int? TagId { get; set; }

        /// <summary>
        /// Status da mensagem.
        /// Este campo é obrigatório e pode representar diferentes estados como Agendada, Enviada, Cancelada.
        /// </summary>
        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// E-mail do contato.
        /// Utilizado para comunicação e, possivelmente, para autenticação.
        /// </summary>
        [Column("email")]
        [StringLength(255)]
        public string? Email { get; set; }

        /// <summary>
        /// Notas adicionais sobre o contato.
        /// Campo opcional. Pode conter informações adicionais ou comentários sobre o contato.
        /// </summary>
        [Column("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Data e hora de criação do contato.
        /// Este campo é utilizado para registrar quando o contato foi criado.
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização do contato.
        /// Este campo é utilizado para registrar quando o contato foi atualizado pela última vez.
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Indica se o contato é ativo no sistema de inteligência artificial.
        /// </summary>
        [Column("ai_active")]
        public int? AiActive { get; set; }

        /// <summary>
        /// Identificador do usuário atribuído ao contato.
        /// </summary>
        [Column("assigned_to")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Indica se o contato é oficial.
        /// </summary>
        [Column("is_official")]
        public bool IsOfficial { get; set; } = false;

        /// <summary>
        /// Prioridade do contato.
        /// </summary>
        [Column("priority")]
        public string? Priority { get; set; } = "normal";

        /// <summary>
        /// Status do contato.
        /// </summary>
        [Column("contact_status")]
        public string ContactStatus { get; set; } = "Novo";

        /// <summary>
        /// Indica se o contato foi visualizado.
        /// </summary>
        [Column("is_viewed")]
        public bool IsViewed { get; set; }

        /// <summary>
        /// Ordem do contato para exibição. Ordenado em ordem ascendente por padrão.
        /// </summary>
        [Column("order")]
        public int Order { get; set; } = 0;

        /// <summary>
        /// Construtor padrão da classe Contact.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public Contact()
        {
            Name = string.Empty;
            Number = string.Empty;
            TagId = 0;
            IsActive = true;
            Priority = "normal";
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsOfficial = false;
            Order = 0;
        }

        /// <summary>
        /// Construtor da classe Contact que inicializa a entidade com valores específicos.
        /// </summary>
        /// <param name="nome">Nome do contato.</param>
        /// <param name="numero">Número de WhatsApp do contato.</param>
        /// <param name="fotoPerfil">URL da foto de perfil do WhatsApp.</param>
        /// <param name="setorId">Identificador do setor associado ao usuário.</param>
        /// <param name="tagId">Identificador da etiqueta associada ao contato.</param>
        /// <param name="status">Status da mensagem.</param>
        /// <param name="email">E-mail do contato.</param>
        /// <param name="anotacoes">Notas adicionais sobre o contato.</param>
        public Contact(string nome, string numero, string? fotoPerfil, int setorId, int? tagId, bool status, string? email, string? anotacoes)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));

            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("O número de WhatsApp não pode ser nulo ou vazio.", nameof(numero));

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                throw new ArgumentException("O e-mail informado não é válido.", nameof(email));

            Name = nome;
            Number = numero;
            AvatarUrl = fotoPerfil;
            SectorId = setorId;
            TagId = tagId;
            IsActive = status;
            Email = email;
            Notes = anotacoes;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsOfficial = false;
            Order = 0;
        }
    }
}
