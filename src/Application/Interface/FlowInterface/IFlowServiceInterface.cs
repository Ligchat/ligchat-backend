using LigChat.Backend.Application.Common.Mappings.FlowResults;
using LigChat.Backend.Domain.DTOs.FlowDto;

namespace LigChat.Data.Interfaces.IServices
{
    public interface IFlowServiceInterface
    {
        /// <summary>
        /// Retrieves all flows for a specific sector and folder.
        /// </summary>
        /// <param name="sectorId">The ID of the sector that owns the flows.</param>
        /// <param name="folderId">The ID of the folder that contains the flows.</param>
        /// <returns>A FlowListResponseDTO containing a collection of flows with a message and status code.</returns>
        FlowListResponse GetAllBySectorIdAndFolderId(int sectorId, int folderId);

        /// <summary>
        /// Retrieves a specific flow by its ID.
        /// </summary>
        /// <param name="id">The ID of the flow to retrieve.</param>
        /// <returns>A SingleFlowResponse with the flow data, or null if the flow is not found.</returns>
        SingleFlowResponse? GetById(string id);

        /// <summary>
        /// Creates a new flow based on the provided CreateFlowRequestDTO for a specific sector and folder.
        /// </summary>
        /// <param name="flowDto">The data for the new flow.</param>
        /// <returns>A SingleFlowResponse indicating the result of the creation operation.</returns>
        SingleFlowResponse? Save(CreateFlowRequestDTO flowDto);

        /// <summary>
        /// Updates an existing flow with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the flow to update.</param>
        /// <param name="flowDto">The updated data for the flow.</param>
        /// <returns>A SingleFlowResponse indicating the result of the update operation.</returns>
        SingleFlowResponse? Update(string id, UpdateFlowRequestDTO flowDto);

        /// <summary>
        /// Deletes a flow based on its ID.
        /// </summary>
        /// <param name="id">The ID of the flow to delete.</param>
        /// <returns>A SingleFlowResponse indicating the result of the deletion operation, or null if the flow is not found.</returns>
        SingleFlowResponse? Delete(string id);
    }
}
