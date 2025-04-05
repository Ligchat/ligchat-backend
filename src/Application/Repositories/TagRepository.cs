using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Repositories
{
    public class TagRepository : ITagRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public TagRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public Tag Save(Tag tag)
        {
            tag.CreatedAt = DateTime.UtcNow;
            tag.UpdatedAt = DateTime.UtcNow;
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag;
        }

        public Tag Update(int id, Tag tag)
        {
            var existingTag = _context.Tags.Find(id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.Description = tag.Description;
                existingTag.Color = tag.Color;
                existingTag.Status = tag.Status;
                existingTag.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
                return existingTag;
            }

            return null;
        }

        public Tag Delete(int id)
        {
            var tag = _context.Tags.Find(id);

            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                return tag;
            }

            return null;
        }

        public Tag? GetById(int id)
        {
            return _context.Tags.Find(id);
        }

        public Tag? GetByName(string name)
        {
            return _context.Tags.FirstOrDefault(tag => tag.Name == name);
        }

        public IEnumerable<Tag> GetAll(int sectorId)
        {
            // Filtrando as tags pelo sectorId
            return _context.Tags
                .Where(tag => tag.SectorId == sectorId) // Supondo que a classe Tag tenha uma propriedade SectorId
                .ToList();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
