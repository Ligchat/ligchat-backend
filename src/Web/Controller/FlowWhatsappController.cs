namespace tests_.src.Web.Controller
{
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
                await _flowService.CreateFlowAsync(flow);
                return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, flow);
            }

            // PUT: api/flowwhatsapp/{id}
            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> UpdateFlow(string id, [FromBody] FlowWhatsapp flowIn)
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }

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
