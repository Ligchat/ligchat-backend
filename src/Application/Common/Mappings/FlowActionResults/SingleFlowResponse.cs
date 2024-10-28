using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único fluxo.
    /// </summary>
    public class SingleFlowResponse : SingleResponse<FlowViewModel>
    {
        public SingleFlowResponse(string message, string code, FlowViewModel flow)
            : base(message, code, flow)
        {
        }

        public SingleFlowResponse() : base() { }
    }
}
