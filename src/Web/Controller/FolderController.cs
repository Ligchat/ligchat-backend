﻿using LigChat.Backend.Application.Common.Mappings.FolderResults;
using LigChat.Backend.Domain.DTOs.FolderDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LigChat.Api.Controllers
{
    [Authorize] // Aplica o filtro de autorização a todo o controller
    [ApiController]
    [Route("api/folders")]
    public class FolderController : ControllerBase, IFolderControllerInterface
    {
        private readonly IFolderServiceInterface _folderService;

        public FolderController(IFolderServiceInterface folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int sectorId)
        {
            var folderListResponse = _folderService.GetAllBySectorId(sectorId);
            if (folderListResponse == null || !folderListResponse.Data.Any())
            {
                return NotFound(new FolderListResponse
                {
                    Message = "No folders found.",
                    Code = "404",
                    Data = new List<FolderViewModel>()
                });
            }

            return Ok(folderListResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var folderResponse = _folderService.GetById(id);

            if (folderResponse == null)
            {
                return NotFound(new SingleFolderResponse
                {
                    Message = "Folder not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(folderResponse);
        }

        [HttpPost]
        public IActionResult Save([FromBody] CreateFolderRequestDTO folderDto)
        {
            if (folderDto == null)
            {
                return BadRequest("Folder data is required.");
            }

            var createdFolderResponse = _folderService.Save(folderDto);

            if (createdFolderResponse == null)
            {
                return BadRequest(new SingleFolderResponse
                {
                    Message = "Folder could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdFolderResponse.Data?.Id },
                createdFolderResponse
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFolderRequestDTO folderDto)
        {
            if (folderDto == null)
            {
                return BadRequest("Folder data is required.");
            }

            var existingFolderResponse = _folderService.GetById(id);

            if (existingFolderResponse == null)
            {
                return NotFound(new SingleFolderResponse
                {
                    Message = "Folder not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedFolderResponse = _folderService.Update(id, folderDto);
            if (updatedFolderResponse == null)
            {
                return BadRequest(new SingleFolderResponse
                {
                    Message = "Folder could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var folderResponse = _folderService.GetById(id);

            if (folderResponse == null)
            {
                return NotFound(new SingleFolderResponse
                {
                    Message = "Folder not found.",
                    Code = "404",
                    Data = null
                });
            }

            _folderService.Delete(id);
            return NoContent();
        }
    }
}
