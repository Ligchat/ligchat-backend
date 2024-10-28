using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única ação do fluxo.
    /// </summary>
    public class SingleFlowActionResponse : SingleResponse<FlowActionViewModel>
    {
        public SingleFlowActionResponse(string message, string code, FlowActionViewModel flowAction)
            : base(message, code, flowAction)
        {
        }

        public SingleFlowActionResponse() : base() { }
    }
}
