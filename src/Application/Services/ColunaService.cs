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

        // Método ajustado para buscar colunas junto com os cards e contatos associados
        public IEnumerable<Coluna> GetAll(int sectorId)
        {
            return _context.Colunas
                .Where(c => c.SectorId == sectorId)
                .Select(c => new Coluna
                {
                    Id = c.Id,
                    Name = c.Name,
                    SectorId = c.SectorId,
                    Cards = _context.Cards
                        .Where(card => card.ColumnId == c.Id)
                        .Select(card => new Card
                        {
                            Id = card.Id,
                            ContactId = card.ContactId,
                            ColumnId = card.ColumnId,
                            LastContact = card.LastContact,
                            Contato = _context.Contatos
                                .Where(contact => contact.Id == card.ContactId)
                                .Select(contact => new Contato
                                {
                                    Id = contact.Id,
                                    Name = contact.Name,
                                    Phone = contact.Phone
                                }).FirstOrDefault() // Obtém o primeiro contato associado ao card
                        }).ToList() // Converte a lista de cards
                })
                .ToList(); // Converte a lista de colunas
        }

        public Coluna GetById(int id)
        {
            return _context.Colunas
                .Where(c => c.Id == id)
                .Select(c => new Coluna
                {
                    Id = c.Id,
                    Name = c.Name,
                    SectorId = c.SectorId,
                    Cards = _context.Cards
                        .Where(card => card.ColumnId == c.Id)
                        .Select(card => new Card
                        {
                            Id = card.Id,
                            ContactId = card.ContactId,
                            ColumnId = card.ColumnId,
                            LastContact = card.LastContact,
                            Contato = _context.Contatos
                                .Where(contact => contact.Id == card.ContactId)
                                .Select(contact => new Contato
                                {
                                    Id = contact.Id,
                                    Name = contact.Name,
                                    Phone = contact.Phone
                                }).FirstOrDefault() // Obtém o primeiro contato associado ao card
                        }).ToList() // Converte a lista de cards
                })
                .FirstOrDefault(); // Retorna a primeira coluna encontrada
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
    }
}
