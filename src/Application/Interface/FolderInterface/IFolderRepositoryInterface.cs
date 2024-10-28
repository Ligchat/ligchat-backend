using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IFolderRepositoryInterface
    {
        // Obtém todos os folders de um setor específico. Retorna uma coleção de folders.
        IEnumerable<Folder> GetAllBySectorId(int sectorId);

        // Obtém um folder pelo ID. Retorna um folder ou null se não encontrado.
        Folder? GetById(int id);

        // Salva um novo folder no repositório. Retorna o folder salvo.
        Folder Save(Folder folder);

        // Atualiza um folder existente pelo ID. Retorna o folder atualizado.
        Folder Update(int id, Folder folder);

        // Deleta um folder pelo ID. Retorna o folder deletado.
        Folder Delete(int id);
    }
}
