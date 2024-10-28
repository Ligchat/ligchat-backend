using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de ações do fluxo.
    /// </summary>
    public class FlowActionListResponse : ListResponse<FlowActionViewModel>
    {
        public FlowActionListResponse(string message, string code, IEnumerable<FlowActionViewModel> flowActions)
            : base(message, code, flowActions)
        {
        }

        public FlowActionListResponse() : base() { }
    }
}
