namespace tests_.src.Application.Interface.ColunaInterface
{
    using System.Collections.Generic;
    using tests_.src.Domain.Entities;

    public interface IColunaService
    {
        IEnumerable<Coluna> GetAll(int sectorId);
        Coluna GetById(int id);
        Coluna Create(Coluna coluna);
        void Update(Coluna coluna);
        void Delete(int id);

        void MoveColuna(int id, int newPosition);
    }
}
