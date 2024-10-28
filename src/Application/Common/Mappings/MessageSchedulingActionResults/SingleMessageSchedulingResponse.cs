using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única mensagem agendada.
    /// </summary>
    public class SingleMessageSchedulingResponse : SingleResponse<MessageSchedulingViewModel>
    {
        public SingleMessageSchedulingResponse(string message, string code, MessageSchedulingViewModel messageScheduling)
            : base(message, code, messageScheduling)
        {
        }

        public SingleMessageSchedulingResponse() : base() { }
    }
}
