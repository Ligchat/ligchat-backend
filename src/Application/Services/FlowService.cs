using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Application.Common.Mappings.FlowResults;
using LigChat.Backend.Domain.DTOs.FlowDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IServices;

namespace LigChat.Api.Services.FlowService
{
    public class FlowService : IFlowServiceInterface
    {
        private readonly IFlowRepositoryInterface _flowRepository;

        public FlowService(IFlowRepositoryInterface flowRepository)
        {
            _flowRepository = flowRepository;
        }

        public FlowListResponse GetAllBySectorIdAndFolderId(int sectorId, int folderId)
        {
            var flows = _flowRepository.GetAllBySectorIdAndFolderId(sectorId, folderId);
            var flowDtos = flows.Select(flow => new FlowViewModel(
                flow.Id,
                flow.Name,
                flow.FolderId,
                flow.SectorId,
                flow.Status
            ));
            return new FlowListResponse("Success", "200", flowDtos);
        }

        public SingleFlowResponse? GetById(string id)
        {
            var flow = _flowRepository.GetById(id);
            if (flow == null)
            {
                return new SingleFlowResponse("Flow not found", "404", null);
            }
            var flowDto = new FlowViewModel(
                flow.Id,
                flow.Name,
                flow.FolderId,
                flow.SectorId,
                flow.Status,
                flow.Folder
            );
            return new SingleFlowResponse("Flow found", "200", flowDto);
        }

        public SingleFlowResponse Save(CreateFlowRequestDTO flowDto)
        {
            // Validação do request
            if (string.IsNullOrWhiteSpace(flowDto.Name))
            {
                return new SingleFlowResponse("Invalid flow data", "400", null);
            }

            // Criação do flow
            var flow = new Flow
            {
                Name = flowDto.Name,
                FolderId = flowDto.FolderId,
                SectorId = flowDto.SectorId,
                Status = flowDto.Status
            };

            var savedFlow = _flowRepository.Save(flow);
            var responseDto = new FlowViewModel(
                savedFlow.Id,
                savedFlow.Name,
                savedFlow.FolderId,
                savedFlow.SectorId,
                savedFlow.Status
            );
            return new SingleFlowResponse("Flow created successfully", "201", responseDto);
        }

        public SingleFlowResponse Update(string id, UpdateFlowRequestDTO flowDto)
        {
            // Validação do request
            if (string.IsNullOrWhiteSpace(flowDto.Name))
            {
                return new SingleFlowResponse("Invalid flow data", "400", null);
            }

            var existingFlow = _flowRepository.GetById(id);
            if (existingFlow == null)
            {
                return new SingleFlowResponse("Flow not found", "404", null);
            }

            // Atualização do flow
            existingFlow.Name = flowDto.Name;
            existingFlow.FolderId = flowDto.FolderId;
            existingFlow.Status = flowDto.Status;
            var savedFlow = _flowRepository.Update(id, existingFlow);

            var responseDto = new FlowViewModel(
                savedFlow.Id,
                savedFlow.Name,
                savedFlow.FolderId,
                savedFlow.SectorId,
                savedFlow.Status
            );
            return new SingleFlowResponse("Flow updated successfully", "200", responseDto);
        }

        public SingleFlowResponse Delete(string id)
        {
            var flow = _flowRepository.GetById(id);
            if (flow == null)
            {
                return new SingleFlowResponse("Flow not found", "404", null);
            }

            _flowRepository.Delete(id);
            var responseDto = new FlowViewModel(
                flow.Id,
                flow.Name,
                flow.FolderId,
                flow.SectorId,
                flow.Status
            );
            return new SingleFlowResponse("Flow deleted successfully", "200", responseDto);
        }
    }
}
