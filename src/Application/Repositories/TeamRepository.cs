using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Repositories
{
    public class TeamRepository : ITeamRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public TeamRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public Team Save(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

        public Team Update(int id, Team team)
        {
            var existingTeam = _context.Teams.Find(id);

            if (existingTeam != null)
            {
                existingTeam.Name = team.Name;
                _context.SaveChanges();
                return existingTeam;
            }

            return null;
        }

        public Team Delete(int id)
        {
            var team = _context.Teams.Find(id);

            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
                return team;
            }

            return null;
        }

        public Team? GetById(int id)
        {
            return _context.Teams.Find(id);
        }

        public Team? GetByName(string name)
        {
            return _context.Teams.FirstOrDefault(team => team.Name == name);
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
