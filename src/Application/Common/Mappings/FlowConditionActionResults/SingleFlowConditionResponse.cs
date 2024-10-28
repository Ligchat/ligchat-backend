using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowConditionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única condição do fluxo.
    /// </summary>
    public class SingleFlowConditionResponse : SingleResponse<FlowConditionViewModel>
    {
        public SingleFlowConditionResponse(string message, string code, FlowConditionViewModel condition)
            : base(message, code, condition)
        {
        }

        public SingleFlowConditionResponse() : base() { }
    }
}
