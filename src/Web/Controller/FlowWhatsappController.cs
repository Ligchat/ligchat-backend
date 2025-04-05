namespace tests_.src.Web.Controller
{
    using global::tests_.src.Application.Services;
    using global::tests_.src.Application.Services.tests_.src.Application.Services;
    using global::tests_.src.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace tests_.src.Web.Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FlowWhatsappController : ControllerBase
        {
            private readonly FlowWhatsappService _flowService;

            public FlowWhatsappController(FlowWhatsappService flowService)
            {
                _flowService = flowService;
            }

            // GET: api/flowwhatsapp
            [HttpGet]
            public async Task<ActionResult<IEnumerable<FlowWhatsapp>>> GetFlows()
            {
                var flows = await _flowService.GetAllFlowsAsync();
                return Ok(flows);
            }

            // GET: api/flowwhatsapp/{id}
            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<FlowWhatsapp>> GetFlow(string id)
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }
                return Ok(flow);
            }

            // POST: api/flowwhatsapp
            [HttpPost]
            public async Task<ActionResult<FlowWhatsapp>> CreateFlow([FromBody] FlowWhatsapp flow)
            {
                if (flow == null)
                {
                    Console.WriteLine("Corpo da requisição está nulo.");
                    return BadRequest("O corpo da requisição é inválido.");
                }

                Console.WriteLine($"Flow recebido: {System.Text.Json.JsonSerializer.Serialize(flow)}");

                await _flowService.CreateFlowAsync(flow);
                return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, flow);
            }

            // PUT: api/flowwhatsapp/{id}
            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> UpdateOrCreateFlow(string id, [FromBody] FlowWhatsapp flowIn)
            {
                if (flowIn == null)
                {
                    return BadRequest("O corpo da requisição é inválido.");
                }

                // Verifica se o fluxo existe
                var flow = await _flowService.GetFlowByIdAsync(id);

                if (flow == null)
                {
                    // Cria um novo fluxo caso ele não exista
                    flowIn.Id = id; // Garante que o ID será o mesmo do parâmetro
                    await _flowService.CreateFlowAsync(flowIn);
                    return CreatedAtAction(nameof(GetFlow), new { id = flowIn.Id }, flowIn);
                }

                // Atualiza o fluxo existente
                await _flowService.UpdateFlowAsync(id, flowIn);
                return NoContent();
            }


            // DELETE: api/flowwhatsapp/{id}
            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> DeleteFlow(string id)
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }

                await _flowService.RemoveFlowAsync(id);

                return NoContent();
            }
        }
    }

}
