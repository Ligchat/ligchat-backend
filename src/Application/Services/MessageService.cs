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

        public IEnumerable<Messeageging> GetAllByContactId(int contatoId)
        {
            return _context.Messeageging
                .Where(m => m.ContatoId == contatoId)
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

            existingMessage.Conteudo = message.Conteudo;
            existingMessage.Tipo = message.Tipo;
            existingMessage.Url = message.Url;
            existingMessage.NomeArquivo = message.NomeArquivo;
            existingMessage.MimeType = message.MimeType;
            existingMessage.IdSetor = message.IdSetor;
            existingMessage.ContatoId = message.ContatoId;
            existingMessage.DataEnvio = message.DataEnvio;
            existingMessage.Enviado = message.Enviado;
            existingMessage.Lido = message.Lido;
            existingMessage.WhatsAppMessageId = message.WhatsAppMessageId;
            existingMessage.IsOfficial = message.IsOfficial;

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
