using LigChat.Backend.Application.Common.Mappings.TagActionResults;
using LigChat.Backend.Domain.DTOs.TagDto;

namespace LigChat.Data.Interfaces.IServices
{
    public interface ITagServiceInterface
    {
        // Retorna uma coleção de tags encapsulada em um DTO com mensagem e código
        TagListResponse GetAll(int sectorId);

        // Retorna uma tag única com base no ID encapsulada em um DTO com mensagem e código, ou nulo se não encontrada
        SingleTagResponse? GetById(int id);

        // Retorna uma tag única com base no nome encapsulada em um DTO com mensagem e código, ou nulo se não encontrada
        SingleTagResponse? GetByName(string name);

        // Cria uma nova tag com base nos dados fornecidos e retorna um DTO com mensagem e código
        SingleTagResponse? Save(CreateTagRequestDTO tagDto);

        // Atualiza uma tag existente com base no ID e nos dados fornecidos, retornando um DTO com mensagem e código
        SingleTagResponse? Update(int id, UpdateTagRequestDTO tagDto);

        // Deleta uma tag com base no ID e retorna um DTO com mensagem e código, ou nulo se não encontrada
        SingleTagResponse? Delete(int id);
    }
}
