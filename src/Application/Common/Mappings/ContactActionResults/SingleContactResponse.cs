using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.ContactActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único contato.
    /// </summary>
    public class SingleContactResponse : SingleResponse<ContactViewModel>
    {
        public SingleContactResponse(string message, string code, ContactViewModel contact)
            : base(message, code, contact)
        {
        }

        public SingleContactResponse() : base() { }
    }
}
