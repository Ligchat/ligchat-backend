using LigChat.Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IMessageSchedulingRepositoryInterface
    {
        Task<IEnumerable<MessageScheduling>> GetAll(int sectorId);
        Task<MessageScheduling?> GetById(int id);
        Task<MessageScheduling> Save(MessageScheduling messageScheduling);
        Task<MessageScheduling> Update(MessageScheduling messageScheduling);
        Task<MessageScheduling?> Delete(int id);
    }
} 