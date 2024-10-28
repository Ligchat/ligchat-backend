using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowConditionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de condições do fluxo.
    /// </summary>
    public class FlowConditionListResponse : ListResponse<FlowConditionViewModel>
    {
        public FlowConditionListResponse(string message, string code, IEnumerable<FlowConditionViewModel> conditions)
            : base(message, code, conditions)
        {
        }

        public FlowConditionListResponse() : base() { }
    }
}
