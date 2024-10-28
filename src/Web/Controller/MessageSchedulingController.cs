using LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/message-schedulings")]
    public class MessageSchedulingController : ControllerBase, IMessageSchedulingControllerInterface
    {
        private readonly IMessageSchedulingServiceInterface _messageSchedulingService;

        public MessageSchedulingController(IMessageSchedulingServiceInterface messageSchedulingService)
        {
            _messageSchedulingService = messageSchedulingService;
        }

        [HttpGet]
        public IActionResult GetAll(int sectorId)
        {
            var messageSchedulingListResponse = _messageSchedulingService.GetAll(sectorId);
            if (messageSchedulingListResponse == null || !messageSchedulingListResponse.Data.Any())
            {
                return NotFound(new MessageSchedulingListResponse
                {
                    Message = "No message schedulings found.",
                    Code = "404",
                    Data = new List<MessageSchedulingViewModel>()
                });
            }

            return Ok(messageSchedulingListResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var messageSchedulingResponse = _messageSchedulingService.GetById(id);

            if (messageSchedulingResponse == null)
            {
                return NotFound(new SingleMessageSchedulingResponse
                {
                    Message = "Message scheduling not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(messageSchedulingResponse);
        }

        [HttpPost]
        public IActionResult Save([FromBody] CreateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            if (messageSchedulingDto == null)
            {
                return BadRequest("Message scheduling data is required.");
            }

            var createdMessageSchedulingResponse = _messageSchedulingService.Save(messageSchedulingDto);

            if (createdMessageSchedulingResponse == null)
            {
                return BadRequest(new SingleMessageSchedulingResponse
                {
                    Message = "Message scheduling could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdMessageSchedulingResponse.Data?.Id },
                createdMessageSchedulingResponse
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateMessageSchedulingRequestDTO messageSchedulingDto)
        {
            if (messageSchedulingDto == null)
            {
                return BadRequest("Message scheduling data is required.");
            }

            var existingMessageSchedulingResponse = _messageSchedulingService.GetById(id);

            if (existingMessageSchedulingResponse == null)
            {
                return NotFound(new SingleMessageSchedulingResponse
                {
                    Message = "Message scheduling not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedMessageSchedulingResponse = _messageSchedulingService.Update(id, messageSchedulingDto);
            if (updatedMessageSchedulingResponse == null)
            {
                return BadRequest(new SingleMessageSchedulingResponse
                {
                    Message = "Message scheduling could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            return Ok(updatedMessageSchedulingResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var messageSchedulingResponse = _messageSchedulingService.GetById(id);

            if (messageSchedulingResponse == null)
            {
                return NotFound(new SingleMessageSchedulingResponse
                {
                    Message = "Message scheduling not found.",
                    Code = "404",
                    Data = null
                });
            }

            _messageSchedulingService.Delete(id);
            return NoContent();
        }
    }
}
