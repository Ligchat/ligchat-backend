using tests_.src.Domain.Entities;

namespace tests_.src.Application.Interface.CardInterface
{
    public interface ICardService
    {
        IEnumerable<Card> GetAll(int sectorId);
        Card GetById(int id);
        Card Create(Card card);
        void Update(Card card);
        void Delete(int id);

        void MoveCard(int cardId, int newColumnId);
    }
}
