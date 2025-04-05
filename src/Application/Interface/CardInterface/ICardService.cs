using tests_.src.Domain.Entities;
using tests_.src.Domain.DTOs.CardDto;

namespace tests_.src.Application.Interface.CardInterface
{
    public interface ICardService
    {
        IEnumerable<Card> GetAll(int sectorId);
        Card GetById(int id);
        Card Create(CreateCardRequestDTO request);
        Card Update(Card card);
        void Delete(int id);

        void MoveCard(int cardId, int newColumnId, int newPosition);
    }
}
