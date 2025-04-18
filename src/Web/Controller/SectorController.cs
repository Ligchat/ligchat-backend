﻿using LigChat.Backend.Application.Common.Mappings.SectorActionResults;
using LigChat.Backend.Domain.DTOs.SectorDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using LigChat.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorController : ControllerBase, ISectorControllerInterface
    {
        private readonly ISectorServiceInterface _sectorService;
        private readonly IUserSectorRepositoryInterface _userSectorRepository;

        public SectorController(ISectorServiceInterface sectorService, IUserSectorRepositoryInterface userSectorRepository)
        {
            _sectorService = sectorService;
            _userSectorRepository = userSectorRepository;
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

            if (createdSectorResponse == null || createdSectorResponse.Code == "400")
            {
                return BadRequest(createdSectorResponse ?? new SingleSectorResponse
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
            if (updatedSectorResponse == null || updatedSectorResponse.Code == "400")
            {
                return BadRequest(updatedSectorResponse ?? new SingleSectorResponse
                {
                    Message = "Sector could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            if (updatedSectorResponse.Code == "404")
            {
                return NotFound(updatedSectorResponse);
            }

            return Ok(updatedSectorResponse);
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

            _userSectorRepository.DeleteAllBySectorId(id);

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
