using LigChat.Backend.Domain.Entities;
    using System.Collections.Generic;
using tests_.src.Domain.Entities;
namespace tests_.src.Application.Interface.Message
{
    namespace Ligchat.Application.Interfaces
    {
        public interface IMessageService
        {
            IEnumerable<Messeageging> GetAllByContactId(int whatsappContactId);
            Messeageging GetById(int id);
            Messeageging Create(Messeageging message);
            bool Update(Messeageging message);
            bool Delete(int id);
        }
    }

}
