using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de etiqueta.
    /// </summary>
    public interface ITagEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome da etiqueta.
        /// Representa o nome pelo qual a etiqueta é conhecida e usada para identificá-la.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Descrição da etiqueta.
        /// Fornece detalhes adicionais sobre a etiqueta, ajudando a entender seu propósito ou uso.
        /// </summary>
        string Description { get; set; }
    }
}
