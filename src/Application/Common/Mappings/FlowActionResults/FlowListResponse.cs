using LigChat.Backend.Application.Common.Mappings.FlowResults;
using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de fluxos.
    /// </summary>
    public class FlowListResponse : ListResponse<FlowViewModel>
    {
        public FlowListResponse(string message, string code, IEnumerable<FlowViewModel> flows)
            : base(message, code, flows)
        {
        }

        public FlowListResponse() : base() { }
    }
}
