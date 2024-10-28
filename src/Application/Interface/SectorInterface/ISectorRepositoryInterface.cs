using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface ISectorRepositoryInterface
    {
        // Obtém todos os setores. Retorna uma coleção de setores.
        IEnumerable<Sector> GetAll(int userId);

        // Obtém um setor pelo ID. Retorna um setor ou null se não encontrado.
        Sector? GetById(int id);

        // Salva um novo setor no repositório. Retorna o setor salvo.
        Sector Save(Sector sector);

        // Atualiza um setor existente pelo ID. Retorna o setor atualizado.
        Sector Update(int id, Sector sector);

        // Deleta um setor pelo ID. Retorna o setor deletado.
        Sector Delete(int id);
    }
}
