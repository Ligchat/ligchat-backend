using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.TeamActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única equipe.
    /// </summary>
    public class SingleTeamResponse : SingleResponse<TeamViewModel>
    {
        public SingleTeamResponse(string message, string code, TeamViewModel team)
            : base(message, code, team)
        {
        }

        public SingleTeamResponse() : base() { }
    }
}
