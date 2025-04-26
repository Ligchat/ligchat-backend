using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LigChat.Backend.Application.Interface.MessageSchedulingInterface
{
    public interface IMessageSchedulingControllerInterface
    {
        Task<IActionResult> GetAll(int sectorId);
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Save(CreateMessageSchedulingRequestDTO messageScheduling);
        Task<IActionResult> Update(int id, UpdateMessageSchedulingRequestDTO messageScheduling);
        Task<IActionResult> Delete(int id);
    }
} 