using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.UserActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de usuários.
    /// </summary>
    public class UserListResponse : ListResponse<UserViewModel>
    {
        public UserListResponse(string message, string code, IEnumerable<UserViewModel> users)
            : base(message, code, users)
        {
        }

        public UserListResponse() : base() { }
    }
}
