using LigChat.Backend.Domain.DTOs.AgentDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigChat.Api.Services
{
    public class AgentService
    {
        private readonly IAgentRepository _agentRepository;

        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<IEnumerable<AgentResponseDTO>> GetAllBySectorIdAsync(int sectorId)
        {
            var agents = await _agentRepository.GetAllBySectorIdAsync(sectorId);
            return agents.Select(MapToResponseDTO);
        }

        public async Task<AgentResponseDTO?> GetByIdAsync(int id)
        {
            var agent = await _agentRepository.GetByIdAsync(id);
            return agent != null ? MapToResponseDTO(agent) : null;
        }

        public async Task<AgentResponseDTO> CreateAsync(CreateAgentRequestDTO dto)
        {
            if (dto.Status && await _agentRepository.ExistsActiveAgentInSectorAsync(dto.SectorId))
            {
                throw new InvalidOperationException("Já existe um agente ativo para este setor.");
            }

            var agent = new Agent
            {
                Name = dto.Name,
                Type = dto.Type,
                ApiKey = dto.ApiKey,
                Model = dto.Model,
                Status = dto.Status,
                Prompt = dto.Prompt,
                SectorId = dto.SectorId
            };

            var createdAgent = await _agentRepository.CreateAsync(agent);
            return MapToResponseDTO(createdAgent);
        }

        public async Task<AgentResponseDTO?> UpdateAsync(int id, UpdateAgentRequestDTO dto)
        {
            var existingAgent = await _agentRepository.GetByIdAsync(id);
            if (existingAgent == null)
            {
                return null;
            }

            if (dto.Status && !existingAgent.Status && 
                await _agentRepository.ExistsActiveAgentInSectorAsync(existingAgent.SectorId))
            {
                throw new InvalidOperationException("Já existe um agente ativo para este setor.");
            }

            existingAgent.Name = dto.Name;
            existingAgent.Type = dto.Type;
            existingAgent.ApiKey = dto.ApiKey;
            existingAgent.Model = dto.Model;
            existingAgent.Status = dto.Status;
            existingAgent.Prompt = dto.Prompt;

            var updatedAgent = await _agentRepository.UpdateAsync(id, existingAgent);
            return updatedAgent != null ? MapToResponseDTO(updatedAgent) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _agentRepository.DeleteAsync(id);
        }

        private static AgentResponseDTO MapToResponseDTO(Agent agent)
        {
            return new AgentResponseDTO
            {
                Id = agent.Id,
                Name = agent.Name,
                Type = agent.Type,
                ApiKey = agent.ApiKey,
                Model = agent.Model,
                Status = agent.Status,
                Prompt = agent.Prompt,
                SectorId = agent.SectorId,
                CreatedAt = agent.CreatedAt,
                UpdatedAt = agent.UpdatedAt
            };
        }
    }
} 