using LigChat.Backend.Web.Extensions.Database;
using System.Collections.Generic;
using System.Linq;
using tests_.src.Application.Interface.Message.Ligchat.Application.Interfaces;
using tests_.src.Domain.Entities;

namespace Ligchat.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly DatabaseConfiguration _context;

        public MessageService(DatabaseConfiguration context)
        {
            _context = context;
        }

        public IEnumerable<Messeageging> GetAllByContactId(int whatsappContactId)
        {
            // Implemente a lógica de busca das mensagens filtradas pelo whatsappContactId.
            return _context.Messeageging
                .Where(m => m.WhatsappContactId == whatsappContactId)
                .ToList();
        }

        public Messeageging GetById(int id)
        {
            return _context.Messeageging.Find(id);
        }

        public Messeageging Create(Messeageging message)
        {
            _context.Messeageging.Add(message);
            _context.SaveChanges();
            return message;
        }

        public bool Update(Messeageging message)
        {
            var existingMessage = _context.Messeageging.Find(message.Id);
            if (existingMessage == null)
            {
                return false;
            }

            // Atualize as propriedades conforme necessário
            existingMessage.Content = message.Content;
            existingMessage.ProfilePicUrl = message.ProfilePicUrl;
            existingMessage.Name = message.Name;
            existingMessage.MessageType = message.MessageType;
            existingMessage.MediaUrl = message.MediaUrl;
            existingMessage.MediaMimeType = message.MediaMimeType;
            existingMessage.MediaFilename = message.MediaFilename;
            existingMessage.IsSent = message.IsSent;
            existingMessage.From = message.From;
            existingMessage.To = message.To;

            _context.Messeageging.Update(existingMessage);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var message = _context.Messeageging.Find(id);
            if (message == null)
            {
                return false;
            }

            _context.Messeageging.Remove(message);
            _context.SaveChanges();
            return true;
        }
    }
}
