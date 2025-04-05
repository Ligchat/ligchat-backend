using System;
using System.Linq;
using LigChat.Backend.Application.Common.Mappings.SectorActionResults;
using LigChat.Backend.Domain.DTOs.SectorDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using tests_.src.Domain.DTOs.SectorDto;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;

namespace LigChat.Api.Services.SectorService
{
    public class SectorService : ISectorServiceInterface
    {
        private readonly ISectorRepositoryInterface _sectorRepository;
        private readonly IUserSectorRepositoryInterface _userSectorRepository;

        public SectorService(ISectorRepositoryInterface sectorRepository, IUserSectorRepositoryInterface userSectorRepository)
        {
            _sectorRepository = sectorRepository;
            _userSectorRepository = userSectorRepository;
        }

        /// <summary>
        /// Obtém todos os setores associados a um usuário específico.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Lista de setores.</returns>
        public SectorListResponse GetAll(int userId)
        {
            // Obtenha todos os UserSector associados ao userId
            var userSectors = _userSectorRepository.GetAllByUserId(userId);

            // Extraia os IDs dos setores associados
            var sectorIds = userSectors.Select(us => us.SectorId).ToList();

            // Obtenha a lista de setores com base nos IDs
            var sectors = _sectorRepository.GetByIds(sectorIds);

            // Monte a lista de DTOs de setores
            var sectorDtos = sectors.Select(sector => new SectorViewModel(
                sector.Id,
                sector.Name,
                sector.Description,
                null, // userBusinessId
                sector.Status,
                sector.PhoneNumberId,
                sector.AccessToken,
                sector.CreatedAt,
                sector.UpdatedAt
            )).ToList();

            return new SectorListResponse("Success", "200", sectorDtos);
        }

        /// <summary>
        /// Obtém um setor específico por seu identificador.
        /// </summary>
        public SingleSectorResponse? GetById(int id)
        {
            var sector = _sectorRepository.GetById(id);
            if (sector == null)
            {
                return new SingleSectorResponse("Sector not found", "404", null);
            }
            var sectorDto = new SectorViewModel(
                sector.Id,
                sector.Name,
                sector.Description,
                null, // userBusinessId
                sector.Status,
                sector.PhoneNumberId,
                sector.AccessToken,
                sector.CreatedAt,
                sector.UpdatedAt
            );
            return new SingleSectorResponse("Success", "200", sectorDto);
        }

        /// <summary>
        /// Salva um novo setor no sistema e cria uma associação com o usuário.
        /// </summary>
        public SingleSectorResponse Save(CreateSectorRequestDTO sectorDto)
        {
            if (string.IsNullOrWhiteSpace(sectorDto.Name))
            {
                return new SingleSectorResponse("Invalid request: Name is required.", "400", null);
            }

            var sector = new Sector
            {
                Name = sectorDto.Name,
                Description = sectorDto.Description,
                Status = sectorDto.Status ?? true,
                PhoneNumberId = sectorDto.PhoneNumberId ?? string.Empty,
                AccessToken = sectorDto.AccessToken ?? string.Empty,
                GoogleClientId = sectorDto.GoogleClientId ?? string.Empty,
                GoogleApiKey = sectorDto.GoogleApiKey ?? string.Empty,
                OAuth2AccessToken = sectorDto.OAuth2AccessToken ?? string.Empty,
                OAuth2RefreshToken = sectorDto.OAuth2RefreshToken ?? string.Empty,
                OAuth2TokenExpiration = sectorDto.OAuth2TokenExpiration,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var savedSector = _sectorRepository.Save(sector);

            // Cria o relacionamento UserSector
            var userSector = new UserSector
            {
                UserId = sectorDto.UserBusinessId,
                SectorId = savedSector.Id,
                IsShared = sectorDto.IsShared
            };
            _userSectorRepository.Save(userSector);

            var responseDto = new SectorViewModel(
                savedSector.Id,
                savedSector.Name,
                savedSector.Description,
                sectorDto.UserBusinessId,
                savedSector.Status,
                savedSector.PhoneNumberId,
                savedSector.AccessToken,
                savedSector.CreatedAt,
                savedSector.UpdatedAt
            );
            return new SingleSectorResponse("Sector created successfully", "201", responseDto);
        }

        /// <summary>
        /// Atualiza as informações de um setor existente.
        /// </summary>
        public SingleSectorResponse Update(int id, UpdateSectorRequestDTO sectorDto)
        {
            var existingSector = _sectorRepository.GetById(id);
            if (existingSector == null)
            {
                return new SingleSectorResponse("Sector not found", "404", null);
            }

            // Atualizando propriedades se fornecidas
            if (!string.IsNullOrWhiteSpace(sectorDto.Name))
                existingSector.Name = sectorDto.Name;

            if (!string.IsNullOrWhiteSpace(sectorDto.Description))
                existingSector.Description = sectorDto.Description;

            if (!string.IsNullOrWhiteSpace(sectorDto.PhoneNumberId))
                existingSector.PhoneNumberId = sectorDto.PhoneNumberId;

            if (!string.IsNullOrWhiteSpace(sectorDto.AccessToken))
                existingSector.AccessToken = sectorDto.AccessToken;

            if (sectorDto.Status.HasValue)
                existingSector.Status = sectorDto.Status.Value;

            if (!string.IsNullOrWhiteSpace(sectorDto.GoogleClientId))
                existingSector.GoogleClientId = sectorDto.GoogleClientId;

            if (!string.IsNullOrWhiteSpace(sectorDto.GoogleApiKey))
                existingSector.GoogleApiKey = sectorDto.GoogleApiKey;

            if (!string.IsNullOrWhiteSpace(sectorDto.OAuth2AccessToken))
                existingSector.OAuth2AccessToken = sectorDto.OAuth2AccessToken;

            if (!string.IsNullOrWhiteSpace(sectorDto.OAuth2RefreshToken))
                existingSector.OAuth2RefreshToken = sectorDto.OAuth2RefreshToken;

            if (sectorDto.OAuth2TokenExpiration.HasValue)
                existingSector.OAuth2TokenExpiration = sectorDto.OAuth2TokenExpiration.Value;

            existingSector.UpdatedAt = DateTime.UtcNow;

            var savedSector = _sectorRepository.Update(id, existingSector);
            var responseDto = new SectorViewModel(
                savedSector.Id,
                savedSector.Name,
                savedSector.Description,
                null, // userBusinessId
                savedSector.Status,
                savedSector.PhoneNumberId,
                savedSector.AccessToken,
                savedSector.CreatedAt,
                savedSector.UpdatedAt
            );
            return new SingleSectorResponse("Sector updated successfully", "200", responseDto);
        }

        public SingleSectorResponse Delete(int sectorId)
        {
            // Exclui todas as associações com o setor no `UserSector`
            _userSectorRepository.Delete(sectorId);

            return new SingleSectorResponse("Associations deleted successfully", "200", null);
        }


        /// <summary>
        /// Verifica se um setor existe para um determinado usuário.
        /// </summary>
        public SectorExistenceResponse SectorExists(int userId, int sectorId)
        {
            var sectorExists = _userSectorRepository.ExistsByUserAndSectorId(userId, sectorId);
            return new SectorExistenceResponse(
                sectorExists ? "Sector exists" : "Sector does not exist",
                sectorExists ? "200" : "404",
                sectorExists
            );
        }
    }
}
