namespace tests_.src.Application.Services
{
    using LigChat.Backend.Web.Extensions.Database;
    using global::tests_.src.Application.Interface.CardInterface;
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using global::tests_.src.Domain.DTOs.CardDto;

    public class CardService : ICardService
    {
        private readonly DatabaseConfiguration _context;

        public CardService(DatabaseConfiguration context)
        {
            _context = context;
        }

        private int GetNextPosition(int columnId)
        {
            var lastPosition = _context.Cards
                .Where(c => c.ColumnId == columnId)
                .Max(c => (int?)c.Position) ?? 0;
            return lastPosition + 1;
        }

        public IEnumerable<Card> GetAll(int sectorId)
        {
            return _context.Cards
                .Where(c => c.SectorId == sectorId)
                .OrderBy(c => c.Position)
                .ToList();
        }

        public Card GetById(int id)
        {
            var card = _context.Cards.Find(id);
            if (card == null)
            {
                throw new Exception($"Card com ID {id} não encontrado");
            }
            return card;
        }

        public Card Create(CreateCardRequestDTO request)
        {
            // Validar se o contato existe
            var contact = _context.Contacts.Find(request.ContactId);
            if (contact == null)
            {
                throw new Exception("O contato especificado não existe");
            }

            // Encontrar a primeira coluna do setor
            var firstColumn = _context.Colunas
                .Where(c => c.SectorId == request.SectorId)
                .OrderBy(c => c.Position)
                .FirstOrDefault();

            if (firstColumn == null)
            {
                throw new Exception("Nenhuma coluna encontrada para o setor especificado");
            }

            // Criar o card
            var card = new Card
            {
                ContactId = request.ContactId,
                ColumnId = firstColumn.Id,
                SectorId = request.SectorId,
                Position = GetNextPosition(firstColumn.Id),
                CreatedAt = DateTime.UtcNow
            };

            _context.Cards.Add(card);
            _context.SaveChanges();

            return card;
        }

        public Card Update(Card card)
        {
            var existingCard = _context.Cards.Find(card.Id);
            if (existingCard == null)
            {
                throw new Exception($"Card com ID {card.Id} não encontrado");
            }

            existingCard.ColumnId = card.ColumnId;
            existingCard.Position = card.Position;
            existingCard.SectorId = card.SectorId;

            _context.SaveChanges();
            return existingCard;
        }

        public void MoveCard(int cardId, int newColumnId, int newPosition)
        {
            var card = _context.Cards.Find(cardId);
            if (card != null)
            {
                var oldColumnId = card.ColumnId;
                card.ColumnId = newColumnId;

                // Reordenar cards na coluna de destino
                var cardsInNewColumn = _context.Cards
                    .Where(c => c.ColumnId == newColumnId && c.Id != cardId)
                    .OrderBy(c => c.Position)
                    .ToList();

                // Ajustar a nova posição se necessário
                if (newPosition < 1)
                {
                    newPosition = 1;
                }
                else if (newPosition > cardsInNewColumn.Count + 1)
                {
                    newPosition = cardsInNewColumn.Count + 1;
                }

                // Inserir o card na nova posição
                cardsInNewColumn.Insert(newPosition - 1, card);

                // Atualizar posições
                for (int i = 0; i < cardsInNewColumn.Count; i++)
                {
                    cardsInNewColumn[i].Position = i + 1;
                }

                // Reordenar cards na coluna de origem se for diferente
                if (oldColumnId != newColumnId)
                {
                    var cardsInOldColumn = _context.Cards
                        .Where(c => c.ColumnId == oldColumnId)
                        .OrderBy(c => c.Position)
                        .ToList();

                    for (int i = 0; i < cardsInOldColumn.Count; i++)
                    {
                        cardsInOldColumn[i].Position = i + 1;
                    }
                }

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
