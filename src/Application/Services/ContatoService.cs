namespace tests_.src.Application.Services
{
    using LigChat.Backend.Web.Extensions.Database;
    using global::tests_.src.Application.Interface.ContatoInterface;
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class ContatoService : IContatoService
    {
        private readonly DatabaseConfiguration _context;

        public ContatoService(DatabaseConfiguration context)
        {
            _context = context;
        }

        public IEnumerable<Contato> GetAll(int sectorId)
        {
            return _context.Contatos.ToList();
        }

        public Contato GetById(int id)
        {
            return _context.Contatos.Find(id);
        }

        public Contato Create(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public void Update(Contato contato)
        {
            _context.Contatos.Update(contato);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato != null)
            {
                _context.Contatos.Remove(contato);
                _context.SaveChanges();
            }
        }
    }

}
