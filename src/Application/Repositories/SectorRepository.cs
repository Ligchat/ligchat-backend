using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Repositories
{
    public class SectorRepository : ISectorRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public SectorRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public Sector Save(Sector sector)
        {
            _context.Sectors.Add(sector);
            _context.SaveChanges();
            return sector;
        }

        public Sector Update(int id, Sector sector)
        {
            var existingSector = _context.Sectors.Find(id);

            if (existingSector != null)
            {
                existingSector.Description = sector.Description;
                _context.SaveChanges();
                return existingSector;
            }

            return null;
        }

        public Sector Delete(int id)
        {
            var sector = _context.Sectors.Find(id);

            if (sector != null)
            {
                _context.Sectors.Remove(sector);
                _context.SaveChanges();
                return sector;
            }

            return null;
        }

        public Sector? GetById(int id)
        {
            return _context.Sectors.Find(id);
        }

        public IEnumerable<Sector> GetAll(int userId)
        {
            return _context.Sectors
                           .Where(s => s.UserBusinessId == userId)
                           .ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
