using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FlowSharedResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de fluxos compartilhados.
    /// </summary>
    public class FlowSharedListResponse : ListResponse<FlowSharedViewModel>
    {
        public FlowSharedListResponse(string message, string code, IEnumerable<FlowSharedViewModel> flowSharedList)
            : base(message, code, flowSharedList)
        {
        }

        public FlowSharedListResponse() : base() { }
    }
}
