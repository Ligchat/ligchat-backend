using LigChat.Backend.Application.Common.Mappings.ContactActionResults;
using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.ContactActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de contatos.
    /// </summary>
    public class ContactListResponse : ListResponse<ContactViewModel>
    {
        public ContactListResponse(string message, string code, IEnumerable<ContactViewModel> contacts)
            : base(message, code, contacts)
        {
        }

        public ContactListResponse() : base() { }
    }
}
