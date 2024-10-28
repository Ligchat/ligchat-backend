using LigChat.Backend.Application.Common.Mappings.FlowResults;
using LigChat.Backend.Domain.DTOs.FlowDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/flows")]
    public class FlowController : ControllerBase, IFlowControllerInterface
    {
        private readonly IFlowServiceInterface _flowService;

        // Constructor to inject the flow service dependency
        public FlowController(IFlowServiceInterface flowService)
        {
            _flowService = flowService;
        }

        /// <summary>
        /// Retrieves all flows.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] int sectorId, [FromQuery] int folderId)
        {
            var flowListResponse = _flowService.GetAllBySectorIdAndFolderId(sectorId, folderId);

            if (flowListResponse == null || !flowListResponse.Data.Any())
            {
                return NotFound(new FlowListResponse
                {
                    Message = "No flows found.",
                    Code = "404",
                    Data = new List<FlowViewModel>()
                });
            }

            return Ok(flowListResponse);
        }

        /// <summary>
        /// Retrieves a flow by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var flowResponse = _flowService.GetById(id);

            if (flowResponse == null)
            {
                return NotFound(new SingleFlowResponse
                {
                    Message = "Flow not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(flowResponse);
        }

        /// <summary>
        /// Creates a new flow.
        /// </summary>
        [HttpPost]
        public IActionResult Save([FromBody] CreateFlowRequestDTO flowDto)
        {
            if (flowDto == null)
            {
                return BadRequest("Flow data is required.");
            }

            var createdFlowResponse = _flowService.Save(flowDto);

            if (createdFlowResponse == null)
            {
                return BadRequest(new SingleFlowResponse
                {
                    Message = "Flow could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdFlowResponse.Data?.Id },
                createdFlowResponse
            );
        }

        /// <summary>
        /// Updates an existing flow.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateFlowRequestDTO flowDto)
        {
            if (flowDto == null)
            {
                return BadRequest("Flow data is required.");
            }

            var existingFlowResponse = _flowService.GetById(id);

            if (existingFlowResponse == null)
            {
                return NotFound(new SingleFlowResponse
                {
                    Message = "Flow not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedFlowResponse = _flowService.Update(id, flowDto);

            if (updatedFlowResponse == null)
            {
                return BadRequest(new SingleFlowResponse
                {
                    Message = "Flow could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a flow by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var flowResponse = _flowService.GetById(id);

            if (flowResponse == null)
            {
                return NotFound(new SingleFlowResponse
                {
                    Message = "Flow not found.",
                    Code = "404",
                    Data = null
                });
            }

            _flowService.Delete(id);
            return NoContent();
        }
    }
}
