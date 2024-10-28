using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de mensagens agendadas.
    /// </summary>
    public class MessageSchedulingListResponse : ListResponse<MessageSchedulingViewModel>
    {
        public MessageSchedulingListResponse(string message, string code, IEnumerable<MessageSchedulingViewModel> messageSchedulings)
            : base(message, code, messageSchedulings)
        {
        }

        public MessageSchedulingListResponse() : base() { }
    }
}
