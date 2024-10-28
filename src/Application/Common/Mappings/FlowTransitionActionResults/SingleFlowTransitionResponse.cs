using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowTransitionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única transição de fluxo.
    /// </summary>
    public class SingleFlowTransitionResponse : SingleResponse<FlowTransitionViewModel>
    {
        public SingleFlowTransitionResponse(string message, string code, FlowTransitionViewModel flowTransition)
            : base(message, code, flowTransition)
        {
        }

        public SingleFlowTransitionResponse() : base() { }
    }
}
