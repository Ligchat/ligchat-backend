using LigChat.Data.Interfaces.IServices;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Application.Common.Mappings.FolderResults;
using LigChat.Backend.Domain.DTOs.FolderDto;
using LigChat.Backend.Domain.Entities;
using System.Linq;

namespace LigChat.Api.Services.FolderService
{
    public class FolderService : IFolderServiceInterface
    {
        private readonly IFolderRepositoryInterface _folderRepository;

        public FolderService(IFolderRepositoryInterface folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public FolderListResponse GetAllBySectorId(int sectorId)
        {
            // Busca as pastas filtradas pelo sectorId
            var folders = _folderRepository.GetAllBySectorId(sectorId);
            var folderDtos = folders.Select(folder => new FolderViewModel(
                folder.Id,
                folder.Name,
                folder.SectorId,
                folder.Status
            )).ToList();

            return new FolderListResponse("Success", "200", folderDtos);
        }

        public SingleFolderResponse? GetById(int id)
        {
            var folder = _folderRepository.GetById(id);
            if (folder == null)
            {
                return new SingleFolderResponse("Folder not found", "404", null);
            }
            var folderDto = new FolderViewModel(
                folder.Id,
                folder.Name,
                folder.SectorId,
                folder.Status
            );
            return new SingleFolderResponse("Folder found", "200", folderDto);
        }

        public SingleFolderResponse Save(CreateFolderRequestDTO folderDto)
        {
            // Validação direta dos dados recebidos
            if (string.IsNullOrWhiteSpace(folderDto.Name))
            {
                return new SingleFolderResponse("Folder name cannot be empty", "400", null);
            }
            if (folderDto.SectorId <= 0)
            {
                return new SingleFolderResponse("Invalid sector ID", "400", null);
            }

            // Mapeamento direto do DTO para a entidade
            var folder = new Folder
            {
                Name = folderDto.Name,
                SectorId = folderDto.SectorId,
                Status = true // Defina o status padrão conforme necessário
            };

            var savedFolder = _folderRepository.Save(folder);
            var responseDto = new FolderViewModel(
                savedFolder.Id,
                savedFolder.Name,
                savedFolder.SectorId,
                savedFolder.Status
            );
            return new SingleFolderResponse("Folder created successfully", "201", responseDto);
        }

        public SingleFolderResponse Update(int id, UpdateFolderRequestDTO folderDto)
        {
            // Validação direta dos dados recebidos
            if (string.IsNullOrWhiteSpace(folderDto.Name))
            {
                return new SingleFolderResponse("Folder name cannot be empty", "400", null);
            }
            if (folderDto.SectorId <= 0)
            {
                return new SingleFolderResponse("Invalid sector ID", "400", null);
            }

            // Busca a pasta existente
            var existingFolder = _folderRepository.GetById(id);
            if (existingFolder == null)
            {
                return new SingleFolderResponse("Folder not found", "404", null);
            }

            // Atualiza os campos necessários
            existingFolder.Name = folderDto.Name;
            existingFolder.SectorId = folderDto.SectorId;

            var savedFolder = _folderRepository.Update(id, existingFolder);
            var responseDto = new FolderViewModel(
                savedFolder.Id,
                savedFolder.Name,
                savedFolder.SectorId,
                savedFolder.Status
            );
            return new SingleFolderResponse("Folder updated successfully", "200", responseDto);
        }

        public SingleFolderResponse Delete(int id)
        {
            var deletedFolder = _folderRepository.Delete(id);
            if (deletedFolder == null)
            {
                return new SingleFolderResponse("Folder not found", "404", null);
            }
            var responseDto = new FolderViewModel(
                deletedFolder.Id,
                deletedFolder.Name,
                deletedFolder.SectorId,
                deletedFolder.Status
            );
            return new SingleFolderResponse("Folder deleted successfully", "200", responseDto);
        }
    }
}
