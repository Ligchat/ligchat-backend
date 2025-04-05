using LigChat.Backend.Domain.Entities;
using System.Collections.Generic;

namespace LigChat.Backend.Domain.Interface
{
    public interface IContactRepository
    {
        void Delete(Contact contact);
        IEnumerable<Contact> GetBySectorId(int sectorId);
    }
} 