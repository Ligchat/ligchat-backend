using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Repositories
{
    public class UserSectorRepository : IUserSectorRepositoryInterface
    {
        private readonly DatabaseConfiguration _context;

        public UserSectorRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifica se existe uma associação entre o usuário e o setor.
        /// </summary>
        public bool ExistsByUserAndSectorId(int userId, int sectorId)
        {
            return _context.Set<UserSector>().Any(us => us.UserId == userId && us.SectorId == sectorId);
        }

        public void DeleteAllByUserId(int userId)
        {
            var userSectors = _context.UserSectors.Where(us => us.UserId == userId).ToList();
            if (userSectors.Any())
            {
                _context.UserSectors.RemoveRange(userSectors);
                _context.SaveChanges();
            }
        }



        public void Delete(int id)
        {
            var userSector = _context.UserSectors.FirstOrDefault(us => us.Id == id);

            if (userSector != null)
            {
                _context.UserSectors.Remove(userSector); 
                _context.SaveChanges(); 
            }
            else
            {
                throw new ArgumentException("No UserSector association found for the given id.");
            }
        }

        public void DeleteAllBySectorId(int sectorId)
        {
            var sectorAssociations = _context.UserSectors.Where(us => us.SectorId == sectorId).ToList();
            if (sectorAssociations.Any())
            {
                _context.UserSectors.RemoveRange(sectorAssociations);
                _context.SaveChanges();
            }
        }


        public List<UserSector> GetByUserId(int userId)
        {
            return _context.UserSectors.Where(us => us.UserId == userId).ToList();
        }

        public IEnumerable<int> GetSectorIdsToKeep(int userId, IEnumerable<int> sectors)
        {
            return _context.UserSectors
                .Where(us => us.UserId == userId && sectors.Contains(us.SectorId))
                .Select(us => us.SectorId)
                .ToList();
        }

        public IEnumerable<UserSector> GetSectorsToRemove(int userId, IEnumerable<int> sectorsToKeep)
        {
            return _context.UserSectors
                .Where(us => us.UserId == userId && us.IsShared && !sectorsToKeep.Contains(us.SectorId))
                .ToList();
        }


        /// <summary>
        /// Obtém todas as associações UserSector de um usuário específico.
        /// </summary>
        public IEnumerable<UserSector> GetAllByUserId(int userId)
        {
            return _context.Set<UserSector>().Where(us => us.UserId == userId).ToList();
        }

        /// <summary>
        /// Salva uma nova associação UserSector.
        /// </summary>
        public UserSector Save(UserSector userSector)
        {
            _context.Set<UserSector>().Add(userSector);
            _context.SaveChanges();
            return userSector;
        }
    }
}
