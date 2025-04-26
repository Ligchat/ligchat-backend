using LigChat.Backend.Application.Common;
using LigChat.Backend.Application.Interface.MessageSchedulingInterface;
using LigChat.Backend.Application.Interface.S3StorageInterface;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using LigChat.Backend.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigChat.Backend.Web.Controllers
{
    [ApiController]
    [Route("api/message-schedulings")]
    public class MessageSchedulingController : ControllerBase, IMessageSchedulingControllerInterface
    {
        private readonly IMessageSchedulingServiceInterface _messageSchedulingService;
        private readonly IS3StorageService _s3Service;
        private readonly IConfiguration _configuration;

        public MessageSchedulingController(
            IMessageSchedulingServiceInterface messageSchedulingService,
            IS3StorageService s3Service,
            IConfiguration configuration)
        {
            _messageSchedulingService = messageSchedulingService;
            _s3Service = s3Service;
            _configuration = configuration;
        }

        [HttpGet("test-s3")]
        public IActionResult TestS3Configuration()
        {
            var config = new
            {
                AccessKey = _configuration["AWS:AccessKey"]?.Substring(0, 5) + "...",
                SecretKey = _configuration["AWS:SecretKey"]?.Substring(0, 5) + "...",
                BucketName = _configuration["AWS:BucketName"],
                Region = _configuration["AWS:Region"]
            };

            return Ok(new { Message = "AWS Configuration", Configuration = config });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int sectorId)
        {
            if (sectorId <= 0)
            {
                return BadRequest(new Response<List<MessageSchedulingViewModel>>
                {
                    Success = false,
                    Message = "Invalid sector ID.",
                    StatusCode = 400,
                    Data = null
                });
            }

            var response = await _messageSchedulingService.GetAll(sectorId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _messageSchedulingService.GetById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreateMessageSchedulingRequestDTO messageScheduling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<MessageSchedulingViewModel>
                {
                    Success = false,
                    Message = "Invalid message scheduling data.",
                    StatusCode = 400,
                    Data = null
                });
            }

            var response = await _messageSchedulingService.SaveAsync(messageScheduling);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMessageSchedulingRequestDTO messageScheduling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<MessageSchedulingViewModel>
                {
                    Success = false,
                    Message = "Invalid message scheduling data.",
                    StatusCode = 400,
                    Data = null
                });
            }

            var response = await _messageSchedulingService.Update(id, messageScheduling);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _messageSchedulingService.Delete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
