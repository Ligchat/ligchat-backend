using LigChat.Backend.Application.Common.Mappings.UserActionResults;
using LigChat.Backend.Application.Interface.UserInterface;
using LigChat.Backend.Domain.DTOs.SectorDto;
using LigChat.Backend.Domain.DTOs.UserDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IServices;
using LigChat.Data.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;
using LigChat.Backend.Application.Repositories;

namespace LigChat.Backend.Web.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase, IUserControllerInterface
    {
        private readonly IUserServiceInterface _userService;
        private readonly ISectorServiceInterface _sectorService;
        private readonly IUserRepositoryInterface _userRepository;
        private readonly IUserSectorRepositoryInterface _userSectorRepository;
        private readonly ISectorRepositoryInterface _sectorRepository;

        public UserController(
            IUserServiceInterface userService, 
            ISectorServiceInterface sectorService, 
            IUserSectorRepositoryInterface userSectorRepository, 
            IUserRepositoryInterface userRepository,
            ISectorRepositoryInterface sectorRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userRepository = userRepository;
            _sectorService = sectorService ?? throw new ArgumentNullException(nameof(sectorService));
            _userSectorRepository = userSectorRepository ?? throw new ArgumentNullException(nameof(userSectorRepository));
            _sectorRepository = sectorRepository ?? throw new ArgumentNullException(nameof(sectorRepository));
        }

        [HttpGet]
        public IActionResult GetAll(int? invitedBy = null)
        {
            var userListResponse = _userService.GetAll(invitedBy);
            if (userListResponse == null || !userListResponse.Data.Any())
            {
                return NotFound(new UserListResponse
                {
                    Message = "No users found.",
                    Code = "404",
                    Data = new List<UserViewModel>()
                });
            }
            return Ok(userListResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userResponse = _userService.GetById(id);
            if (userResponse == null)
            {
                return NotFound(new SingleUserResponse
                {
                    Message = "User not found.",
                    Code = "404",
                    Data = null
                });
            }
            return Ok(userResponse);
        }

        [HttpPost]
        public IActionResult Save([FromBody] CreateUserRequestDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            var createdUserResponse = _userService.Save(userDto);
            if (createdUserResponse == null)
            {
                return BadRequest(new SingleUserResponse
                {
                    Message = "User could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            if (userDto.Sectors != null)
            {
                foreach (var sectorDto in userDto.Sectors)
                {
                    var sectorExistenceResponse = _sectorService.SectorExists(createdUserResponse.Data.Id, sectorDto.Id);
                    if (sectorExistenceResponse.Exists)
                    {
                        continue;
                    }

                    var existingSectorResponse = _sectorService.GetById(sectorDto.Id);
                    if (existingSectorResponse != null && sectorDto.IsShared)
                    {
                        var userSector = new UserSector
                        {
                            UserId = createdUserResponse.Data.Id,
                            SectorId = sectorDto.Id,
                            IsShared = true
                        };
                        _userSectorRepository.Save(userSector);
                    }
                }
            }

            return CreatedAtAction(nameof(GetById), new { id = createdUserResponse.Data?.Id }, createdUserResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserRequestDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            var existingUserResponse = _userService.GetById(id);
            if (existingUserResponse == null)
            {
                return NotFound(new SingleUserResponse
                {
                    Message = "User not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedUserResponse = _userService.Update(id, userDto);
            if (updatedUserResponse == null)
            {
                return BadRequest(new SingleUserResponse
                {
                    Message = "User could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            if (userDto.Sectors != null)
            {
                // Obter os setores atuais do usuário
                var currentUserSectors = _userSectorRepository.GetAllByUserId(updatedUserResponse.Data.Id);

                // Remover todos os setores atuais
                foreach (var sector in currentUserSectors)
                {
                    _userSectorRepository.Delete(sector.Id);
                }

                // Adicionar os novos setores
                foreach (var sectorDto in userDto.Sectors)
                {
                    var newSector = new UserSector
                    {
                        UserId = updatedUserResponse.Data.Id,
                        SectorId = sectorDto.Id,
                        IsShared = true
                    };
                    _userSectorRepository.Save(newSector);
                }
            }

            return Ok(updatedUserResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userResponse = _userService.GetById(id);
            if (userResponse == null)
            {
                return NotFound(new SingleUserResponse
                {
                    Message = "User not found.",
                    Code = "404",
                    Data = null
                });
            }

            _userService.Delete(id);
            return NoContent();
        }
    }
}
