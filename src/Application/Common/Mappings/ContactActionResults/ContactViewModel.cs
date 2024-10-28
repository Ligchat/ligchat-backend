namespace LigChat.Backend.Application.Common.Mappings.ContactActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do contato na API.
    /// </summary>
    public class ContactViewModel
    {
        // Identificador único do contato
        public int Id { get; }

        // Nome do contato
        public string Name { get; }

        // Identificador da etiqueta associada ao contato
        public int TagId { get; }

        // Número de WhatsApp do contato
        public string PhoneWhatsapp { get; }

        // URL da foto de perfil do WhatsApp (opcional)
        public string? ProfilePicUrl { get; }


        // E-mail do contato
        public string Email { get; }

        // Notas adicionais sobre o contato (opcional)
        public string? Notes { get; }

        // Status do contato
        public bool Status { get; }

        // Identificador do setor associado ao contato (opcional)
        public int? SectorId { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public ContactViewModel()
        {
            Id = 0;
            Name = string.Empty;
            TagId = 0;
            PhoneWhatsapp = string.Empty;
            ProfilePicUrl = null; // Inicializa ProfilePicUrl como nulo
            Email = string.Empty;
            Notes = null;
            Status = false;
            SectorId = null;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public ContactViewModel(int id, string name, int tagId, string phoneWhatsapp, string? profilePicUrl = null, string email = "", string? notes = null, bool status = false, int? sectorId = null)
        {
            Id = id;
            Name = name;
            TagId = tagId;
            PhoneWhatsapp = phoneWhatsapp;
            ProfilePicUrl = profilePicUrl; // Adiciona ProfilePicUrl
            Email = email;
            Notes = notes;
            Status = status;
            SectorId = sectorId;
        }
    }
}
