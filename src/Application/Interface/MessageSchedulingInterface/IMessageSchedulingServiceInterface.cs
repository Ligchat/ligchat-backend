using LigChat.Backend.Application.Common;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using LigChat.Backend.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigChat.Backend.Application.Interface.MessageSchedulingInterface
{
    public interface IMessageSchedulingServiceInterface
    {
        Task<Response<List<MessageSchedulingViewModel>>> GetAll(int sectorId);
        Task<Response<MessageSchedulingViewModel>> GetById(int id);
        Task<Response<MessageSchedulingViewModel>> SaveAsync(CreateMessageSchedulingRequestDTO messageSchedulingDto);
        Task<Response<MessageSchedulingViewModel>> Update(int id, UpdateMessageSchedulingRequestDTO messageSchedulingDto);
        Task<Response<bool>> Delete(int id);
    }
} 