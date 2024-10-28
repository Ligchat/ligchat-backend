using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowNodeResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único nó de fluxo.
    /// </summary>
    public class SingleFlowNodeResponse : SingleResponse<FlowNodeViewModel>
    {
        public SingleFlowNodeResponse(string message, string code, FlowNodeViewModel node)
            : base(message, code, node)
        {
        }

        public SingleFlowNodeResponse() : base() { }
    }
}
