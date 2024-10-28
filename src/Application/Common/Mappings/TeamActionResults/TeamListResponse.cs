using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.TeamActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de equipes.
    /// </summary>
    public class TeamListResponse : ListResponse<TeamViewModel>
    {
        public TeamListResponse(string message, string code, IEnumerable<TeamViewModel> teams)
            : base(message, code, teams)
        {
        }

        public TeamListResponse() : base() { }
    }
}
