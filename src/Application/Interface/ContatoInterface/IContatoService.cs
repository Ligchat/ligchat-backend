namespace tests_.src.Application.Interface.ContatoInterface
{
    using LigChat.Backend.Domain.Entities;
    using System.Collections.Generic;

    public interface IContatoService
    {
        IEnumerable<Contact> GetAll(int sectorId);
        Contact GetById(int id);
        Contact Create(Contact contato);
        void Update(Contact contato);
        void Delete(int id);
    }
}
