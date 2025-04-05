using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface ISectorRepositoryInterface
    {
        // Obtém todos os setores associados a um usuário.
        IEnumerable<Sector> GetAll(int userId);

        // Obtém um setor pelo ID. Retorna um setor ou null se não encontrado.
        Sector? GetById(int id);

        // Obtém uma lista de setores com base em uma coleção de IDs.
        IEnumerable<Sector> GetByIds(IEnumerable<int> sectorIds);

        // Salva um novo setor no repositório. Retorna o setor salvo.
        Sector Save(Sector sector);

        // Atualiza um setor existente pelo ID. Retorna o setor atualizado.
        Sector Update(int id, Sector sector);


        // Deleta um setor pelo ID. Retorna o setor deletado.
        Sector Delete(int id);
    }
}
