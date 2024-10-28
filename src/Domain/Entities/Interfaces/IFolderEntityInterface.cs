using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade Folder, que representa uma pasta.
    /// </summary>
    public interface IFolderEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome da pasta.
        /// Representa o nome da pasta onde dados ou arquivos são armazenados.
        /// </summary>
        string Name { get; set; }
    }
}
