using LigChat.Backend.Application.Common.Mappings.FolderResults;
using LigChat.Backend.Domain.DTOs.FolderDto;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Data.Interfaces.IServices
{
    public interface IFolderServiceInterface
    {
        // Retorna uma coleção de pastas encapsulada em um DTO com mensagem e código
        FolderListResponse GetAllBySectorId(int sectorId);

        // Retorna uma pasta única com base no ID encapsulada em um DTO com mensagem e código, ou nulo se não encontrado
        SingleFolderResponse? GetById(int id);

        // Cria uma nova pasta com base nos dados fornecidos e retorna um DTO com mensagem e código
        SingleFolderResponse? Save(CreateFolderRequestDTO folderDto);

        // Atualiza uma pasta existente com base no ID e nos dados fornecidos, retornando um DTO com mensagem e código
        SingleFolderResponse? Update(int id, UpdateFolderRequestDTO folderDto);

        // Deleta uma pasta com base no ID e retorna um DTO com mensagem e código, ou nulo se não encontrado
        SingleFolderResponse? Delete(int id);
    }
}
