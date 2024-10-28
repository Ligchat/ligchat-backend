using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigChat.Data.Repositories
{
    public class FolderRepository : IFolderRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public FolderRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public Folder Save(Folder folder)
        {
            _context.Folders.Add(folder);
            _context.SaveChanges();
            return folder;
        }

        public Folder Update(int id, Folder folder)
        {
            var existingFolder = _context.Folders.Find(id);

            if (existingFolder != null)
            {
                existingFolder.Name = folder.Name;
                existingFolder.Status = folder.Status;
                existingFolder.SectorId = folder.SectorId;
                existingFolder.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
                return existingFolder;
            }

            return null;
        }

        public Folder Delete(int id)
        {
            var folder = _context.Folders.Find(id);

            if (folder != null)
            {
                _context.Folders.Remove(folder);
                _context.SaveChanges();
                return folder;
            }

            return null;
        }

        public Folder? GetById(int id)
        {
            return _context.Folders.Find(id);
        }

        public IEnumerable<Folder> GetAllBySectorId(int sectorId)
        {
            return _context.Folders
                           .Where(f => f.SectorId == sectorId)
                           .ToList();
        }

        public IEnumerable<Folder> GetAll()
        {
            return _context.Folders.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
