namespace tests_.src.Application.Services
{
    using LigChat.Backend.Web.Extensions.Database;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using LigChat.Backend.Domain.Entities;
    using global::tests_.src.Domain.Entities;
    using global::tests_.src.Application.Interface.ColunaInterface;

    public class ColunaService : IColunaService
    {
        private readonly DatabaseConfiguration _context;

        public ColunaService(DatabaseConfiguration context)
        {
            _context = context;
        }

        public IEnumerable<Coluna> GetAll(int sectorId)
        {
            try
            {
                Console.WriteLine($"Buscando colunas para o setor {sectorId}");
                var result = _context.Set<Coluna>()
                    .Where(c => c.SectorId == sectorId)
                    .OrderBy(c => c.Position)
                    .AsNoTracking()
                    .Select(c => new Coluna 
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SectorId = c.SectorId,
                        Position = c.Position
                    })
                    .ToList();

                Console.WriteLine($"Encontradas {result.Count} colunas");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar colunas: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public Coluna GetById(int id)
        {
            return _context.Set<Coluna>()
                .Where(c => c.Id == id)
                .Include(c => c.Cards)
                    .ThenInclude(card => card.Contact)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Coluna Create(Coluna coluna)
        {
            _context.Colunas.Add(coluna);
            _context.SaveChanges();
            return coluna;
        }

        public void Update(Coluna coluna)
        {
            _context.Colunas.Update(coluna);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var coluna = _context.Colunas.Find(id);
            if (coluna != null)
            {
                _context.Colunas.Remove(coluna);
                _context.SaveChanges();
            }
        }

        public void MoveColuna(int colunaId, int newPosition)
        {
            var coluna = _context.Colunas.Find(colunaId);
            if (coluna != null)
            {
                var colunas = _context.Colunas
                    .Where(c => c.SectorId == coluna.SectorId && c.Id != colunaId)
                    .OrderBy(c => c.Position)
                    .ToList();

                coluna.Position = newPosition;
                colunas.Insert(newPosition - 1, coluna);

                for (int i = 0; i < colunas.Count; i++)
                {
                    colunas[i].Position = i + 1;
                }

                _context.SaveChanges();
            }
        }

        public bool TryDelete(int id, out string error)
        {
            var coluna = _context.Colunas.Find(id);
            if (coluna == null)
            {
                error = "Coluna não encontrada";
                return false;
            }
            var hasCards = _context.Cards.Any(c => c.ColumnId == id);
            if (hasCards)
            {
                error = "Não é possível excluir a coluna pois existem cards associados a ela";
                return false;
            }
            _context.Colunas.Remove(coluna);
            _context.SaveChanges();
            error = null;
            return true;
        }

        public Coluna UpdateName(int id, string newName)
        {
            var coluna = _context.Colunas.Find(id);
            if (coluna == null)
            {
                return null;
            }
            coluna.Name = newName;
            _context.SaveChanges();
            return coluna;
        }
    }
}
