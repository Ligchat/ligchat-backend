using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.UserActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único usuário.
    /// </summary>
    public class SingleUserResponse : SingleResponse<UserViewModel>
    {
        public SingleUserResponse(string message, string code, UserViewModel user)
            : base(message, code, user)
        {
        }

        public SingleUserResponse() : base() { }
    }
}
