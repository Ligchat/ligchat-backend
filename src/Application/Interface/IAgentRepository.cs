using LigChat.Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAllBySectorIdAsync(int sectorId);
        Task<Agent?> GetByIdAsync(int id);
        Task<Agent> CreateAsync(Agent agent);
        Task<Agent?> UpdateAsync(int id, Agent agent);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsActiveAgentInSectorAsync(int sectorId);
    }
} 