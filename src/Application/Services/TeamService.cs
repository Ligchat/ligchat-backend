using LigChat.Backend.Application.Common.Mappings.TeamActionResults;
using LigChat.Backend.Domain.DTOs.TeamDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;

namespace LigChat.Api.Services.TeamService
{
    public class TeamService : ITeamServiceInterface
    {
        private readonly ITeamRepositoryInterface _teamRepository;

        public TeamService(ITeamRepositoryInterface teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public TeamListResponse GetAll()
        {
            var teams = _teamRepository.GetAll();
            var teamDtos = teams.Select(team => new TeamViewModel(team.Id, team.Name, team.SectorId, team.Permissions, team.Status));
            return new TeamListResponse("Success", "200", teamDtos);
        }

        public SingleTeamResponse? GetById(int id)
        {
            var team = _teamRepository.GetById(id);
            if (team == null)
            {
                return new SingleTeamResponse("Team not found", "404", null);
            }
            var teamDto = new TeamViewModel(team.Id, team.Name, team.SectorId, team.Permissions, team.Status);
            return new SingleTeamResponse("Success", "200", teamDto);
        }

        public SingleTeamResponse? GetByName(string name)
        {
            var team = _teamRepository.GetByName(name);
            if (team == null)
            {
                return new SingleTeamResponse("Team not found", "404", null);
            }
            var teamDto = new TeamViewModel(team.Id, team.Name, team.SectorId, team.Permissions, team.Status);
            return new SingleTeamResponse("Success", "200", teamDto);
        }

        public SingleTeamResponse Save(CreateTeamRequestDTO teamDto)
        {
            // Valida��o manual (implementar a l�gica de valida��o aqui)
            if (string.IsNullOrWhiteSpace(teamDto.Name) || teamDto.SectorId <= 0)
            {
                return new SingleTeamResponse("Invalid request", "400", null);
            }

            // Cria��o do objeto Team
            var team = new Team
            {
                Name = teamDto.Name,
                SectorId = teamDto.SectorId,
                Permissions = teamDto.Permissions,
                Status = teamDto.Status
            };

            var savedTeam = _teamRepository.Save(team);
            var responseDto = new TeamViewModel(savedTeam.Id, savedTeam.Name, savedTeam.SectorId, savedTeam.Permissions, savedTeam.Status);
            return new SingleTeamResponse("Team created successfully", "201", responseDto);
        }

        public SingleTeamResponse Update(int id, UpdateTeamRequestDTO teamDto)
        {
            // Valida��o manual (implementar a l�gica de valida��o aqui)
            if (string.IsNullOrWhiteSpace(teamDto.Name) || teamDto.SectorId <= 0)
            {
                return new SingleTeamResponse("Invalid request", "400", null);
            }

            var existingTeam = _teamRepository.GetById(id);
            if (existingTeam == null)
            {
                return new SingleTeamResponse("Team not found", "404", null);
            }

            // Atualizando o Team
            existingTeam.Name = teamDto.Name;
            existingTeam.SectorId = teamDto.SectorId;
            existingTeam.Permissions = teamDto.Permissions;
            existingTeam.Status = teamDto.Status;

            var savedTeam = _teamRepository.Update(id, existingTeam);
            var responseDto = new TeamViewModel(savedTeam.Id, savedTeam.Name, savedTeam.SectorId, savedTeam.Permissions, savedTeam.Status);
            return new SingleTeamResponse("Team updated successfully", "200", responseDto);
        }

        public SingleTeamResponse Delete(int id)
        {
            var deletedTeam = _teamRepository.Delete(id);
            if (deletedTeam == null)
            {
                return new SingleTeamResponse("Team not found", "404", null);
            }
            var responseDto = new TeamViewModel(deletedTeam.Id, deletedTeam.Name, deletedTeam.SectorId, deletedTeam.Permissions, deletedTeam.Status);
            return new SingleTeamResponse("Team deleted successfully", "200", responseDto);
        }
    }
}
