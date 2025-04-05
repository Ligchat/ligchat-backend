using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LigChat.Data.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly DatabaseConfiguration _context;

        public AgentRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agent>> GetAllBySectorIdAsync(int sectorId)
        {
            return await _context.Agents
                .Where(a => a.SectorId == sectorId)
                .ToListAsync();
        }

        public async Task<Agent?> GetByIdAsync(int id)
        {
            return await _context.Agents.FindAsync(id);
        }

        public async Task<Agent> CreateAsync(Agent agent)
        {
            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Agent?> UpdateAsync(int id, Agent agent)
        {
            var existingAgent = await _context.Agents.FindAsync(id);
            if (existingAgent == null)
            {
                return null;
            }

            existingAgent.Name = agent.Name;
            existingAgent.Type = agent.Type;
            existingAgent.ApiKey = agent.ApiKey;
            existingAgent.Model = agent.Model;
            existingAgent.Status = agent.Status;
            existingAgent.Prompt = agent.Prompt;
            existingAgent.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingAgent;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return false;
            }

            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsActiveAgentInSectorAsync(int sectorId)
        {
            return await _context.Agents
                .AnyAsync(a => a.SectorId == sectorId && a.Status);
        }
    }
} 