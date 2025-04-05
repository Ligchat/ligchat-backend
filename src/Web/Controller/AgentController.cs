using LigChat.Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LigChat.Api.Services;
using LigChat.Backend.Domain.DTOs.AgentDto;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly AgentService _agentService;

        public AgentController(AgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet("sector/{sectorId}")]
        public async Task<ActionResult<IEnumerable<AgentResponseDTO>>> GetAllBySector(int sectorId)
        {
            var agents = await _agentService.GetAllBySectorIdAsync(sectorId);
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgentResponseDTO>> GetById(int id)
        {
            var agent = await _agentService.GetByIdAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        [HttpPost]
        public async Task<ActionResult<AgentResponseDTO>> Create([FromBody] CreateAgentRequestDTO dto)
        {
            try
            {
                var createdAgent = await _agentService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdAgent.Id }, createdAgent);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AgentResponseDTO>> Update(int id, [FromBody] UpdateAgentRequestDTO dto)
        {
            try
            {
                var updatedAgent = await _agentService.UpdateAsync(id, dto);
                if (updatedAgent == null)
                {
                    return NotFound();
                }
                return Ok(updatedAgent);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _agentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 