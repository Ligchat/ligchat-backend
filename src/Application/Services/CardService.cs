namespace tests_.src.Application.Services
{
    using LigChat.Backend.Web.Extensions.Database;
    using global::tests_.src.Application.Interface.CardInterface;
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class CardService : ICardService
    {
        private readonly DatabaseConfiguration _context;

        public CardService(DatabaseConfiguration context)
        {
            _context = context;
        }

        public IEnumerable<Card> GetAll(int sectorId)
        {
            return _context.Cards.ToList();
        }

        public Card GetById(int id)
        {
            return _context.Cards.Find(id);
        }

        public Card Create(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
            return card;
        }

        public void Update(Card card)
        {
            _context.Cards.Update(card);
            _context.SaveChanges();
        }

        public void MoveCard(int cardId, int newColumnId)
        {
            var card = _context.Cards.Find(cardId);
            if (card != null)
            {
                card.ColumnId = newColumnId;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var card = _context.Cards.Find(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                _context.SaveChanges();
            }
        }
    }
}
