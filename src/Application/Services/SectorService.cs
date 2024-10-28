using LigChat.Backend.Application.Common.Mappings.SectorActionResults;
using LigChat.Backend.Domain.DTOs.SectorDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;

namespace LigChat.Api.Services.SectorService
{
    public class SectorService : ISectorServiceInterface
    {
        private readonly ISectorRepositoryInterface _sectorRepository;

        public SectorService(ISectorRepositoryInterface sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public SectorListResponse GetAll(int userId)
        {
            var sectors = _sectorRepository.GetAll(userId);
            var sectorDtos = sectors.Select(sector => new SectorViewModel(
                sector.Id,
                sector.Name,
                sector.Description,
                sector.UserBusinessId,
                sector.Status,
                sector.PhoneNumberId, // Usando campos atuais
                sector.AccessToken,
                sector.CreatedAt,  // Passando createdAt
                sector.UpdatedAt   // Passando updatedAt
            ));
            return new SectorListResponse("Success", "200", sectorDtos);
        }

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
                sector.UserBusinessId,
                sector.Status,
                sector.PhoneNumberId, // Usando campos atuais
                sector.AccessToken,
                sector.CreatedAt,  // Passando createdAt
                sector.UpdatedAt   // Passando updatedAt
            );
            return new SingleSectorResponse("Success", "200", sectorDto);
        }

        public SingleSectorResponse Save(CreateSectorRequestDTO sectorDto)
        {
            // Validação manual (implementar a lógica de validação aqui)
            if (string.IsNullOrWhiteSpace(sectorDto.Name))
            {
                return new SingleSectorResponse("Invalid request", "400", null);
            }

            // Criação do objeto Sector
            var sector = new Sector
            {
                Name = sectorDto.Name,
                Description = sectorDto.Description,
                UserBusinessId = sectorDto.UserBusinessId,
                Status = sectorDto.Status,
                PhoneNumberId = sectorDto.PhoneNumberId, // Usando campos atuais
                AccessToken = sectorDto.AccessToken,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var savedSector = _sectorRepository.Save(sector);
            var responseDto = new SectorViewModel(
                savedSector.Id,
                savedSector.Name,
                savedSector.Description,
                savedSector.UserBusinessId,
                savedSector.Status,
                savedSector.PhoneNumberId, // Usando campos atuais
                savedSector.AccessToken,
                savedSector.CreatedAt,  // Passando createdAt
                savedSector.UpdatedAt   // Passando updatedAt
            );
            return new SingleSectorResponse("Sector created successfully", "201", responseDto);
        }

        public SingleSectorResponse Update(int id, UpdateSectorRequestDTO sectorDto)
        {
            // Validação manual (implementar a lógica de validação aqui)
            if (string.IsNullOrWhiteSpace(sectorDto.Name))
            {
                return new SingleSectorResponse("Invalid request", "400", null);
            }

            var existingSector = _sectorRepository.GetById(id);
            if (existingSector == null)
            {
                return new SingleSectorResponse("Sector not found", "404", null);
            }

            // Atualizando o setor
            existingSector.Name = sectorDto.Name;
            existingSector.Description = sectorDto.Description;
            existingSector.UserBusinessId = sectorDto.UserBusinessId;
            existingSector.Status = sectorDto.Status;
            existingSector.PhoneNumberId = sectorDto.PhoneNumberId; // Usando campos atuais
            existingSector.AccessToken = sectorDto.AccessToken;
            existingSector.UpdatedAt = DateTime.UtcNow;

            var savedSector = _sectorRepository.Update(id, existingSector);
            var responseDto = new SectorViewModel(
                savedSector.Id,
                savedSector.Name,
                savedSector.Description,
                savedSector.UserBusinessId,
                savedSector.Status,
                savedSector.PhoneNumberId, // Usando campos atuais
                savedSector.AccessToken,
                savedSector.CreatedAt,  // Passando createdAt
                savedSector.UpdatedAt   // Passando updatedAt
            );
            return new SingleSectorResponse("Sector updated successfully", "200", responseDto);
        }

        public SingleSectorResponse Delete(int id)
        {
            var deletedSector = _sectorRepository.Delete(id);
            if (deletedSector == null)
            {
                return new SingleSectorResponse("Sector not found", "404", null);
            }
            var responseDto = new SectorViewModel(
                deletedSector.Id,
                deletedSector.Name,
                deletedSector.Description,
                deletedSector.UserBusinessId,
                deletedSector.Status,
                deletedSector.PhoneNumberId, // Usando campos atuais
                deletedSector.AccessToken,
                deletedSector.CreatedAt,  // Passando createdAt
                deletedSector.UpdatedAt   // Passando updatedAt
            );
            return new SingleSectorResponse("Sector deleted successfully", "200", responseDto);
        }
    }
}
