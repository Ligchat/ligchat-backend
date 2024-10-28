using Microsoft.AspNetCore.Mvc;
using tests_.src.Domain.Entities;
using tests_.src.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tests_.src.Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariablesController : ControllerBase
    {
        private readonly VariablesService _variablesService;

        public VariablesController(VariablesService variablesService)
        {
            _variablesService = variablesService;
        }

        [HttpGet("sector/{sectorId}")]
        public async Task<IActionResult> GetAllBySector(int sectorId)
        {
            var result = await _variablesService.GetAllBySectorIdAsync(sectorId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _variablesService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Variables variable)
        {
            var createdVariable = await _variablesService.CreateAsync(variable);
            return CreatedAtAction(nameof(GetAllBySector), new { sectorId = createdVariable.SectorId }, createdVariable);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Variables variable)
        {
            var result = await _variablesService.EditAsync(id, variable);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _variablesService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
