using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowTransitionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de transições de fluxo.
    /// </summary>
    public class FlowTransitionListResponse : ListResponse<FlowTransitionViewModel>
    {
        public FlowTransitionListResponse(string message, string code, IEnumerable<FlowTransitionViewModel> flowTransitionList)
            : base(message, code, flowTransitionList)
        {
        }

        public FlowTransitionListResponse() : base() { }
    }
}
