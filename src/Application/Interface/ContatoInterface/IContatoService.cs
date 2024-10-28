namespace tests_.src.Application.Interface.ContatoInterface
{
    using System.Collections.Generic;
    using tests_.src.Domain.Entities;

    public interface IContatoService
    {
        IEnumerable<Contato> GetAll(int sectorId);
        Contato GetById(int id);
        Contato Create(Contato contato);
        void Update(Contato contato);
        void Delete(int id);
    }
}
