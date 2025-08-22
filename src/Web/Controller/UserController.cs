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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult Save([FromBody] CreateUserRequestDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            // Extrair o ID do usuário do token JWT
            var currentUserId = GetUserIdFromClaims();
            if (!currentUserId.HasValue)
            {
                return Unauthorized(new { Message = "Invalid token or missing user ID." });
            }

            // Definir automaticamente o campo InvitedBy com o ID do usuário atual
            userDto.InvitedBy = currentUserId.Value;

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
        [Authorize]
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

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public IActionResult GetCurrentUserProfile()
        {
            var userId = GetUserIdFromClaims();
            if (!userId.HasValue)
            {
                return Unauthorized(new { Message = "Invalid token or missing user ID." });
            }

            var profileResponse = _userService.GetProfile(userId.Value);
            if (profileResponse.Code == "404")
            {
                return NotFound(profileResponse);
            }

            return Ok(profileResponse);
        }

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public IActionResult UpdateCurrentUserProfile([FromBody] UpdateProfileRequestDTO profileDto)
        {
            if (profileDto == null)
            {
                return BadRequest("Profile data is required.");
            }

            var userId = GetUserIdFromClaims();
            if (!userId.HasValue)
            {
                return Unauthorized(new { Message = "Invalid token or missing user ID." });
            }

            var updatedProfileResponse = _userService.UpdateProfile(userId.Value, profileDto);
            if (updatedProfileResponse.Code == "404")
            {
                return NotFound(updatedProfileResponse);
            }
            
            if (updatedProfileResponse.Code == "400")
            {
                return BadRequest(updatedProfileResponse);
            }

            return Ok(updatedProfileResponse);
        }

        [HttpDelete("{id}")]
        [Authorize]
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
