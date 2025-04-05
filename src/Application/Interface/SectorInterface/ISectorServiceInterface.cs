using LigChat.Backend.Application.Common.Mappings.SectorActionResults;
using LigChat.Backend.Domain.DTOs.SectorDto;
using tests_.src.Domain.DTOs.SectorDto;

namespace LigChat.Data.Interfaces.IServices
{
    public interface ISectorServiceInterface
    {
        // Retorna uma coleção de setores associados a um usuário, encapsulada em um DTO com mensagem e código
        SectorListResponse GetAll(int userId);

        // Retorna um setor único com base no ID, encapsulado em um DTO com mensagem e código, ou nulo se não encontrado
        SingleSectorResponse? GetById(int id);

        // Verifica se um setor já existe para um determinado usuário
        SectorExistenceResponse SectorExists(int userId, int sectorId);

        // Cria um novo setor com base nos dados fornecidos e retorna um DTO com mensagem e código
        SingleSectorResponse? Save(CreateSectorRequestDTO sectorDto);

        // Atualiza um setor existente com base no ID e nos dados fornecidos, retornando um DTO com mensagem e código
        SingleSectorResponse? Update(int id, UpdateSectorRequestDTO sectorDto);

        // Deleta um setor com base no ID e retorna um DTO com mensagem e código, ou nulo se não encontrado
        SingleSectorResponse? Delete(int id);
    }
}
