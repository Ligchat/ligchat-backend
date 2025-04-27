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
using LigChat.Backend.Web.Extensions.Database;

namespace LigChat.Api.Services.SectorService
{
    public class SectorService : ISectorServiceInterface
    {
        private readonly ISectorRepositoryInterface _sectorRepository;
        private readonly IUserSectorRepositoryInterface _userSectorRepository;
        private readonly DatabaseConfiguration _context;

        public SectorService(ISectorRepositoryInterface sectorRepository, IUserSectorRepositoryInterface userSectorRepository, DatabaseConfiguration context)
        {
            _sectorRepository = sectorRepository;
            _userSectorRepository = userSectorRepository;
            _context = context;
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
                sector.UpdatedAt,
                sector.IsOfficial
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
                sector.UpdatedAt,
                sector.IsOfficial
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

            if (!string.IsNullOrWhiteSpace(sectorDto.PhoneNumberId) && _sectorRepository.ExistsByPhoneNumberId(sectorDto.PhoneNumberId))
            {
                return new SingleSectorResponse("Invalid request: Phone number ID is already in use.", "400", null);
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
                IsOfficial = sectorDto.IsOfficial ?? false,
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
                savedSector.UpdatedAt,
                savedSector.IsOfficial
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

            if (!string.IsNullOrWhiteSpace(sectorDto.PhoneNumberId) && 
                sectorDto.PhoneNumberId != existingSector.PhoneNumberId && 
                _sectorRepository.ExistsByPhoneNumberId(sectorDto.PhoneNumberId))
            {
                return new SingleSectorResponse("Invalid request: Phone number ID is already in use.", "400", null);
            }

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

            if (sectorDto.IsOfficial.HasValue)
                existingSector.IsOfficial = sectorDto.IsOfficial.Value;

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
                savedSector.UpdatedAt,
                savedSector.IsOfficial
            );
            return new SingleSectorResponse("Sector updated successfully", "200", responseDto);
        }

        public SingleSectorResponse Delete(int sectorId)
        {
            // 1. Atualizar contatos do setor para remover tag_id
            var contatos = _context.Contacts.Where(c => c.SectorId == sectorId).ToList();
            foreach (var contato in contatos)
            {
                contato.TagId = null;
            }
            _context.SaveChanges();

            // 2. Remover as tags do setor
            var tags = _context.Tags.Where(t => t.SectorId == sectorId).ToList();
            foreach (var tag in tags)
            {
                _context.Tags.Remove(tag);
            }
            _context.SaveChanges();

            // 3. Remover os cards dos contatos do setor
            var contatoIds = contatos.Select(c => c.Id).ToList();
            var cards = _context.Cards.Where(card => contatoIds.Contains(card.ContactId ?? 0)).ToList();
            foreach (var card in cards)
            {
                _context.Cards.Remove(card);
            }
            _context.SaveChanges();

            // 4. Remover as colunas do setor
            var colunas = _context.Colunas.Where(col => col.SectorId == sectorId).ToList();
            foreach (var coluna in colunas)
            {
                _context.Colunas.Remove(coluna);
            }
            _context.SaveChanges();

            // 5. Remover as mensagens dos contatos do setor
            var mensagens = _context.Messeageging.Where(m => contatoIds.Contains(m.ContatoId)).ToList();
            foreach (var mensagem in mensagens)
            {
                _context.Messeageging.Remove(mensagem);
            }
            _context.SaveChanges();

            // 6. Remover os agentes do setor
            var agentes = _context.Agents.Where(a => a.SectorId == sectorId).ToList();
            foreach (var agente in agentes)
            {
                _context.Agents.Remove(agente);
            }
            _context.SaveChanges();

            // 7. Remover os contatos do setor
            foreach (var contato in contatos)
            {
                _context.Contacts.Remove(contato);
            }
            _context.SaveChanges();

            // 8. Remover os registros de user_sectors associados ao setor
            var userSectors = _context.UserSectors.Where(us => us.SectorId == sectorId).ToList();
            foreach (var us in userSectors)
            {
                _context.UserSectors.Remove(us);
            }
            _context.SaveChanges();

            // 9. Remover o setor
            var setor = _context.Sectors.Find(sectorId);
            if (setor != null)
            {
                _context.Sectors.Remove(setor);
                _context.SaveChanges();
            }

            return new SingleSectorResponse("Sector and all related data deleted successfully", "200", null);
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
