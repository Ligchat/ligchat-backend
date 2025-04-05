using LigChat.Backend.Domain.Entities;
using System.Collections.Generic;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IUserSectorRepositoryInterface
    {
        // Verifica se existe uma associação entre o usuário e o setor
        bool ExistsByUserAndSectorId(int userId, int sectorId);

        // Obtém todas as associações UserSector de um usuário específico
        IEnumerable<UserSector> GetAllByUserId(int userId);

        // Obtém uma lista de associações específicas de setores de um usuário
        List<UserSector> GetByUserId(int userId);

        // Salva uma nova associação UserSector
        UserSector Save(UserSector userSector);

        // Remove uma associação específica UserSector pelo ID
        void Delete(int id);

        void DeleteAllByUserId(int userId);

        IEnumerable<UserSector> GetSectorsToRemove(int userId, IEnumerable<int> sectorsToKeep);

        IEnumerable<int> GetSectorIdsToKeep(int userId, IEnumerable<int> sectors);

        void DeleteAllBySectorId(int sectorId);
    }
}
