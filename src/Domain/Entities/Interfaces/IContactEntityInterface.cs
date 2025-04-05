using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de contato.
    /// </summary>
    public interface IContactEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do contato.
        /// Representa o nome completo ou o nome pelo qual o contato é conhecido.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Identificador da etiqueta associada ao contato.
        /// Referencia a tag que categoriza o contato.
        /// </summary>
        int? TagId { get; set; }

        /// <summary>
        /// Número de WhatsApp do contato.
        /// Utilizado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// E-mail do contato.
        /// Utilizado para comunicação e, possivelmente, para autenticação.
        /// </summary>
        string? Email { get; set; }

        /// <summary>
        /// Notas adicionais sobre o contato.
        /// Campo opcional. Pode conter informações adicionais ou comentários sobre o contato.
        /// </summary>
        string? Notes { get; set; }

        /// <summary>
        /// Indica se o contato está ativo.
        /// </summary>
        bool IsActive { get; set; }
    }
}
