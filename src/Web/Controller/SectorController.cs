using LigChat.Backend.Application.Common.Mappings.SectorActionResults;
using LigChat.Backend.Domain.DTOs.SectorDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorController : ControllerBase, ISectorControllerInterface
    {
        private readonly ISectorServiceInterface _sectorService;

        public SectorController(ISectorServiceInterface sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var userId = GetUserIdFromClaims();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token or missing user ID." });
            }

            var sectorListResponse = _sectorService.GetAll(userId.Value);
            if (sectorListResponse == null || !sectorListResponse.Data.Any())
            {
                return NotFound(new SectorListResponse
                {
                    Message = "No sectors found.",
                    Code = "404",
                    Data = new List<SectorViewModel>()
                });
            }

            return Ok(sectorListResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sectorResponse = _sectorService.GetById(id);

            if (sectorResponse == null)
            {
                return NotFound(new SingleSectorResponse
                {
                    Message = "Sector not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(sectorResponse);
        }

        [HttpPost]
        public IActionResult Save([FromBody] CreateSectorRequestDTO sectorDto)
        {
            if (sectorDto == null)
            {
                return BadRequest("Sector data is required.");
            }

            sectorDto.Status = true;

            var createdSectorResponse = _sectorService.Save(sectorDto);

            if (createdSectorResponse == null)
            {
                return BadRequest(new SingleSectorResponse
                {
                    Message = "Sector could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdSectorResponse.Data?.Id },
                createdSectorResponse
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSectorRequestDTO sectorDto)
        {
            if (sectorDto == null)
            {
                return BadRequest("Sector data is required.");
            }

            var existingSectorResponse = _sectorService.GetById(id);

            if (existingSectorResponse == null)
            {
                return NotFound(new SingleSectorResponse
                {
                    Message = "Sector not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedSectorResponse = _sectorService.Update(id, sectorDto);
            if (updatedSectorResponse == null)
            {
                return BadRequest(new SingleSectorResponse
                {
                    Message = "Sector could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sectorResponse = _sectorService.GetById(id);

            if (sectorResponse == null)
            {
                return NotFound(new SingleSectorResponse
                {
                    Message = "Sector not found.",
                    Code = "404",
                    Data = null
                });
            }

            _sectorService.Delete(id);
            return NoContent();
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User?.Claims.FirstOrDefault(x => x.Type == "userId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            return null;
        }

    }


}
