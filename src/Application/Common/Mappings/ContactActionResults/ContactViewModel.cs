namespace LigChat.Backend.Application.Common.Mappings.ContactActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do contato na API.
    /// </summary>
    public class ContactViewModel
    {
        // Identificador único do contato
        public int Id { get; set; }

        // Nome do contato
        public string Name { get; set; }

        // Identificador da etiqueta associada ao contato
        public int? TagId { get; set; }

        // Número de WhatsApp do contato
        public string Number { get; set; }

        // URL da foto de perfil do WhatsApp (opcional)
        public string? AvatarUrl { get; set; }

        // E-mail do contato
        public string? Email { get; set; }

        // Notas adicionais sobre o contato (opcional)
        public string? Notes { get; set; }

        // Identificador do setor associado ao contato (opcional)
        public int? SectorId { get; set; }

        // Status do contato
        public bool IsActive { get; set; }

        // Prioridade do contato
        public string? Priority { get; set; }

        // Status do contato
        public string? ContactStatus { get; set; }

        // Identificador do contato no AI
        public int AiActive { get; set; }

        // Identificador do usuário atribuído ao contato
        public int? AssignedTo { get; set; }

        // Data de criação do contato
        public DateTime CreatedAt { get; set; }

        // Data de atualização do contato
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Indica se o contato é oficial.
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// Indica se o contato foi visualizado.
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// Ordem do contato para exibição, ordenado de forma ascendente por padrão.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public ContactViewModel()
        {
            Id = 0;
            Name = string.Empty;
            TagId = null;
            Number = string.Empty;
            AvatarUrl = null; // Inicializa AvatarUrl como nulo
            Email = null;
            Notes = null;
            IsActive = false;
            SectorId = null;
            Priority = null;
            ContactStatus = null;
            AiActive = 0;
            AssignedTo = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsOfficial = false;
            IsViewed = false;
            Order = 0;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public ContactViewModel(int id, string name, int? tagId, string number, string? avatarUrl = null, string? email = null, string? notes = null, bool isActive = false, int? sectorId = null, string? priority = null, string contactStatus = "", int aiActive = 0, int? assignedTo = null, DateTime? createdAt = null, DateTime? updatedAt = null, bool isOfficial = false, bool isViewed = false, int order = 0)
        {
            Id = id;
            Name = name;
            TagId = tagId;
            Number = number;
            AvatarUrl = avatarUrl; // Adiciona AvatarUrl
            Email = email;
            Notes = notes;
            IsActive = isActive;
            SectorId = sectorId;
            Priority = priority;
            ContactStatus = contactStatus;
            AiActive = aiActive;
            AssignedTo = assignedTo;
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
            IsOfficial = isOfficial;
            IsViewed = isViewed;
            Order = order;
        }
    }
}
