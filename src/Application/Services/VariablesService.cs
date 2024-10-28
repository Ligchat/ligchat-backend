using tests_.src.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LigChat.Backend.Web.Extensions.Database;

namespace tests_.src.Domain.Services
{
    public class VariablesService
    {
        private readonly DatabaseConfiguration _context;

        public VariablesService(DatabaseConfiguration context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Variables>> GetAllBySectorIdAsync(int sectorId)
        {
            return await _context.Variables.Where(v => v.SectorId == sectorId).ToListAsync();
        }

        public async Task<IEnumerable<Variables>> GetAllAsync()
        {
            return await _context.Variables.ToListAsync();
        }

        public async Task<Variables> CreateAsync(Variables variable)
        {
            await _context.Variables.AddAsync(variable);
            await _context.SaveChangesAsync();
            return variable;
        }

        public async Task<bool> EditAsync(int id, Variables variable)
        {
            var existingVariable = await _context.Variables.FindAsync(id);
            if (existingVariable == null)
            {
                return false;
            }

            existingVariable.Name = variable.Name;
            existingVariable.Value = variable.Value;
            existingVariable.SectorId = variable.SectorId;

            _context.Variables.Update(existingVariable);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var variable = await _context.Variables.FindAsync(id);
            if (variable == null)
            {
                return false;
            }

            _context.Variables.Remove(variable);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
