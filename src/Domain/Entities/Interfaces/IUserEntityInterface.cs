using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de usuário.
    /// </summary>
    public interface IUserEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do usuário.
        /// Representa o nome completo ou o nome pelo qual o usuário é conhecido.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// E-mail do usuário.
        /// Utilizado para comunicação e autenticação do usuário.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// URL do avatar do usuário.
        /// Opcional. Utilizado para armazenar a URL da imagem de perfil do usuário.
        /// </summary>
        string? AvatarUrl { get; set; }

        /// <summary>
        /// Número de WhatsApp do usuário.
        /// Usado para contato via WhatsApp. Pode conter apenas números e alguns caracteres especiais.
        /// </summary>
        string PhoneWhatsapp { get; set; }
    }
}
