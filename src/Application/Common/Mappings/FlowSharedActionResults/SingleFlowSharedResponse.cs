using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowSharedResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único fluxo compartilhado.
    /// </summary>
    public class SingleFlowSharedResponse : SingleResponse<FlowSharedViewModel>
    {
        public SingleFlowSharedResponse(string message, string code, FlowSharedViewModel flowShared)
            : base(message, code, flowShared)
        {
        }

        public SingleFlowSharedResponse() : base() { }
    }
}
