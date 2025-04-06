using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using System.Linq;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Api.Services.MessageSchedulingService
{
    public class MessageSchedulingService : IMessageSchedulingServiceInterface
    {
        private readonly IMessageSchedulingRepositoryInterface _messageSchedulingRepository;

        public MessageSchedulingService(IMessageSchedulingRepositoryInterface messageSchedulingRepository)
        {
            _messageSchedulingRepository = messageSchedulingRepository;
        }

        public MessageSchedulingListResponse GetAll(int sectorId)
        {
            var messageSchedulings = _messageSchedulingRepository.GetAll(sectorId);
            var messageSchedulingDtos = messageSchedulings.Select(ms => new MessageSchedulingViewModel(
                ms.Id,
                ms.Name,
                ms.MessageText,
                ms.SendDate,
                ms.ContactId,
                ms.SectorId,
                ms.Status,
                ms.TagIds,
                ms.CreatedAt,
                ms.UpdatedAt
            )).ToList();
            return new MessageSchedulingListResponse("Success", "200", messageSchedulingDtos);
        }

        public SingleMessageSchedulingResponse? GetById(int id)
        {
            var messageScheduling = _messageSchedulingRepository.GetById(id);
            if (messageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }
            var messageSchedulingDto = new MessageSchedulingViewModel(
                messageScheduling.Id,
                messageScheduling.Name,
                messageScheduling.MessageText,
                messageScheduling.SendDate,
                messageScheduling.ContactId,
                messageScheduling.SectorId,
                messageScheduling.Status,
                messageScheduling.TagIds,
                messageScheduling.CreatedAt,
                messageScheduling.UpdatedAt
            );
            return new SingleMessageSchedulingResponse("Success", "200", messageSchedulingDto);
        }

        public SingleMessageSchedulingResponse Save(CreateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            if (string.IsNullOrWhiteSpace(messageSchedulingDto.Name))
            {
                return new SingleMessageSchedulingResponse("Invalid request: Name is required", "400", null);
            }

            var messageScheduling = new MessageScheduling
            {
                Name = messageSchedulingDto.Name,
                MessageText = messageSchedulingDto.MessageText,
                SendDate = messageSchedulingDto.SendDate,
                ContactId = messageSchedulingDto.ContactId,
                SectorId = messageSchedulingDto.SectorId,
                Status = messageSchedulingDto.Status,
                TagIds = messageSchedulingDto.TagIds,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var savedMessageScheduling = _messageSchedulingRepository.Save(messageScheduling);
            var responseDto = new MessageSchedulingViewModel(
                savedMessageScheduling.Id,
                savedMessageScheduling.Name,
                savedMessageScheduling.MessageText,
                savedMessageScheduling.SendDate,
                savedMessageScheduling.ContactId,
                savedMessageScheduling.SectorId,
                savedMessageScheduling.Status,
                savedMessageScheduling.TagIds,
                savedMessageScheduling.CreatedAt,
                savedMessageScheduling.UpdatedAt
            );
            return new SingleMessageSchedulingResponse("Message Scheduling created successfully", "201", responseDto);
        }

        public SingleMessageSchedulingResponse Update(int id, UpdateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            var existingMessageScheduling = _messageSchedulingRepository.GetById(id);
            if (existingMessageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }

            if (messageSchedulingDto.Name != null)
                existingMessageScheduling.Name = messageSchedulingDto.Name;

            if (messageSchedulingDto.MessageText != null)
                existingMessageScheduling.MessageText = messageSchedulingDto.MessageText;

            if (!string.IsNullOrEmpty(messageSchedulingDto.SendDate))
                existingMessageScheduling.SendDate = messageSchedulingDto.SendDate;

            if (messageSchedulingDto.ContactId.HasValue)
                existingMessageScheduling.ContactId = messageSchedulingDto.ContactId.Value;

            if (messageSchedulingDto.SectorId.HasValue)
                existingMessageScheduling.SectorId = messageSchedulingDto.SectorId.Value;

            if (messageSchedulingDto.Status.HasValue)
                existingMessageScheduling.Status = messageSchedulingDto.Status.Value;

            if (messageSchedulingDto.TagIds != null)
                existingMessageScheduling.TagIds = messageSchedulingDto.TagIds;

            existingMessageScheduling.UpdatedAt = DateTime.UtcNow;

            var savedMessageScheduling = _messageSchedulingRepository.Update(id, existingMessageScheduling);
            var responseDto = new MessageSchedulingViewModel(
                savedMessageScheduling.Id,
                savedMessageScheduling.Name,
                savedMessageScheduling.MessageText,
                savedMessageScheduling.SendDate,
                savedMessageScheduling.ContactId,
                savedMessageScheduling.SectorId,
                savedMessageScheduling.Status,
                savedMessageScheduling.TagIds,
                savedMessageScheduling.CreatedAt,
                savedMessageScheduling.UpdatedAt
            );
            return new SingleMessageSchedulingResponse("Message Scheduling updated successfully", "200", responseDto);
        }

        public SingleMessageSchedulingResponse Delete(int id)
        {
            var deletedMessageScheduling = _messageSchedulingRepository.Delete(id);
            if (deletedMessageScheduling == null)
            {
                return new SingleMessageSchedulingResponse("Message Scheduling not found", "404", null);
            }
            var responseDto = new MessageSchedulingViewModel(
                deletedMessageScheduling.Id,
                deletedMessageScheduling.Name,
                deletedMessageScheduling.MessageText,
                deletedMessageScheduling.SendDate,
                deletedMessageScheduling.ContactId,
                deletedMessageScheduling.SectorId,
                deletedMessageScheduling.Status,
                deletedMessageScheduling.TagIds,
                deletedMessageScheduling.CreatedAt,
                deletedMessageScheduling.UpdatedAt
            );
            return new SingleMessageSchedulingResponse("Message Scheduling deleted successfully", "200", responseDto);
        }
    }
}
