using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigChat.Data.Repositories
{
    public class MessageSchedulingRepository : IMessageSchedulingRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public MessageSchedulingRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        public MessageScheduling Save(MessageScheduling messageScheduling)
        {
            _context.MessageSchedulings.Add(messageScheduling);
            _context.SaveChanges();
            return messageScheduling;
        }

        public MessageScheduling Update(int id, MessageScheduling messageScheduling)
        {
            var existingMessageScheduling = _context.MessageSchedulings.Find(id);

            if (existingMessageScheduling != null)
            {
                existingMessageScheduling.Name = messageScheduling.Name;
                existingMessageScheduling.MessageText = messageScheduling.MessageText;
                existingMessageScheduling.SendDate = messageScheduling.SendDate;
                existingMessageScheduling.ContactId = messageScheduling.ContactId;
                existingMessageScheduling.SectorId = messageScheduling.SectorId;
                existingMessageScheduling.Status = messageScheduling.Status;
                existingMessageScheduling.TagIds = messageScheduling.TagIds;
                existingMessageScheduling.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return existingMessageScheduling;
            }

            return null;
        }

        public MessageScheduling Delete(int id)
        {
            var messageScheduling = _context.MessageSchedulings.Find(id);

            if (messageScheduling != null)
            {
                _context.MessageSchedulings.Remove(messageScheduling);
                _context.SaveChanges();
                return messageScheduling;
            }

            return null;
        }

        public MessageScheduling? GetById(int id)
        {
            return _context.MessageSchedulings.Find(id);
        }

        public IEnumerable<MessageScheduling> GetBySendDate(string sendDate)
        {
            return _context.MessageSchedulings.Where(ms => ms.SendDate == sendDate).ToList();
        }

        public IEnumerable<MessageScheduling> GetAll(int sectorId)
        {
            Console.WriteLine($"Buscando mensagens agendadas para o setor {sectorId}");
            var result = _context.MessageSchedulings
                               .Where(ms => ms.SectorId == sectorId)
                               .ToList();
            Console.WriteLine($"Encontradas {result.Count} mensagens agendadas");
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
