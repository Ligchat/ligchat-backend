using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única tag.
    /// </summary>
    public class SingleTagResponse : SingleResponse<TagViewModel>
    {
        public SingleTagResponse(string message, string code, TagViewModel tag)
            : base(message, code, tag)
        {
        }

        public SingleTagResponse() : base() { }
    }
}
