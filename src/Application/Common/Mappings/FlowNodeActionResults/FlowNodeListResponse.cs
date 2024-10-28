using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowNodeResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de nós de fluxo.
    /// </summary>
    public class FlowNodeListResponse : ListResponse<FlowNodeViewModel>
    {
        public FlowNodeListResponse(string message, string code, IEnumerable<FlowNodeViewModel> nodes)
            : base(message, code, nodes)
        {
        }

        public FlowNodeListResponse() : base() { }
    }
}
