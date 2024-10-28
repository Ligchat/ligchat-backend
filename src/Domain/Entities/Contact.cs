using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigChat.Backend.Domain.Entities.Interfaces;

namespace LigChat.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de contato no sistema.
    /// Mapeia a tabela "whatsapp_contacts" no banco de dados e herda de BaseEntity.
    /// </summary>
    [Table("whatsapp_contacts")]
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
        [Column("name")]
        [Required]
        [MaxLength(100)] // Limita o comprimento do nome a 100 caracteres
        public string Name { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada ao contato.
        /// Referencia a tag que categoriza o contato.
        /// </summary>
        [Column("tag_id")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ID da etiqueta deve ser um valor positivo.")]
        public int TagId { get; set; }

        /// <summary>
        /// Número de WhatsApp do contato.
        /// Utilizado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        [Column("number")]
        [Required]
        [MaxLength(15)] // Limita o comprimento do número a 15 caracteres
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "O número de WhatsApp é inválido.")]
        public string Number { get; set; }

        /// <summary>
        /// URL da foto de perfil do WhatsApp (opcional).
        /// </summary>
        [Column("profile_pic_url")]
        [MaxLength(500)] // Limita o comprimento da URL a 500 caracteres
        public string? ProfilePicUrl { get; set; }

        /// <summary>
        /// E-mail do contato.
        /// Utilizado para comunicação e, possivelmente, para autenticação.
        /// </summary>
        [Column("email")]
        [Required]
        [MaxLength(255)] // Limita o comprimento do e-mail a 255 caracteres
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Notas adicionais sobre o contato.
        /// Campo opcional. Pode conter informações adicionais ou comentários sobre o contato.
        /// </summary>
        [Column("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Status da mensagem.
        /// Este campo é obrigatório e pode representar diferentes estados como Agendada, Enviada, Cancelada.
        /// </summary>
        [Column("status")]
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Identificador do setor associado ao usuário.
        /// Este campo é opcional e representa o ID do setor ao qual o usuário pertence, se aplicável.
        /// </summary>
        [Column("setor_id")]
        [Range(0, int.MaxValue, ErrorMessage = "O ID do setor deve ser um valor não negativo.")]
        public int? SectorId { get; set; }

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
        /// Construtor padrão da classe Contact.
        /// Inicializa as propriedades com valores padrão.
        /// </summary>
        public Contact()
        {
            Name = string.Empty; // Inicializa Name com uma string vazia
            TagId = 0; // Inicializa TagId com 0
            Number = string.Empty; // Inicializa PhoneWhatsapp com uma string vazia
            ProfilePicUrl = null; // Inicializa ProfilePicUrl como nulo
            Email = string.Empty; // Inicializa Email com uma string vazia
            Status = true; // Inicializa Status com true
            CreatedAt = DateTime.UtcNow; // Inicializa CreatedAt com a data e hora atuais em UTC
            UpdatedAt = DateTime.UtcNow; // Inicializa UpdatedAt com a data e hora atuais em UTC
        }

        /// <summary>
        /// Construtor da classe Contact que inicializa a entidade com valores específicos.
        /// </summary>
        /// <param name="name">Nome do contato.</param>
        /// <param name="tagId">Identificador da etiqueta associada ao contato.</param>
        /// <param name="number">Número de WhatsApp do contato.</param>
        /// <param name="profilePicUrl">URL da foto de perfil do WhatsApp.</param>
        /// <param name="address">Endereço do contato.</param>
        /// <param name="email">E-mail do contato.</param>
        /// <param name="notes">Notas adicionais sobre o contato.</param>
        public Contact(string name, int tagId, string number, string? profilePicUrl, string email, string? notes = null, int? sectorId = null, bool status = true)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));

            if (tagId <= 0)
                throw new ArgumentOutOfRangeException(nameof(tagId), "O ID da etiqueta deve ser um valor positivo.");

            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("O número de WhatsApp não pode ser nulo ou vazio.", nameof(number));

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                throw new ArgumentException("O e-mail informado não é válido.", nameof(email));

            Name = name;
            TagId = tagId;
            Number = number;
            ProfilePicUrl = profilePicUrl; // Adiciona ProfilePicUrl
            Email = email;
            Notes = notes;
            Status = status; // Inicializa Status com true
            CreatedAt = DateTime.UtcNow; // Inicializa CreatedAt com a data e hora atuais em UTC
            UpdatedAt = DateTime.UtcNow; // Inicializa UpdatedAt com a data e hora atuais em UTC
        }
    }
}
