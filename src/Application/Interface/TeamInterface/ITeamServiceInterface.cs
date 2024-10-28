using LigChat.Backend.Application.Common.Mappings.TeamActionResults;
using LigChat.Backend.Domain.DTOs.TeamDto;

namespace LigChat.Data.Interfaces.IServices
{
    public interface ITeamServiceInterface
    {
        // Retorna uma coleção de equipes encapsulada em um DTO com mensagem e código
        TeamListResponse GetAll();

        // Retorna uma equipe única com base no ID encapsulada em um DTO com mensagem e código, ou nulo se não encontrada
        SingleTeamResponse? GetById(int id);

        // Retorna uma equipe única com base no nome encapsulada em um DTO com mensagem e código, ou nulo se não encontrada
        SingleTeamResponse? GetByName(string name);

        // Cria uma nova equipe com base nos dados fornecidos e retorna um DTO com mensagem e código
        SingleTeamResponse? Save(CreateTeamRequestDTO teamDto);

        // Atualiza uma equipe existente com base no ID e nos dados fornecidos, retornando um DTO com mensagem e código
        SingleTeamResponse? Update(int id, UpdateTeamRequestDTO teamDto);

        // Deleta uma equipe com base no ID e retorna um DTO com mensagem e código, ou nulo se não encontrada
        SingleTeamResponse? Delete(int id);
    }
}
