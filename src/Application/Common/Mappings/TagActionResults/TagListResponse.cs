using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de tags.
    /// </summary>
    public class TagListResponse : ListResponse<TagViewModel>
    {
        public TagListResponse(string message, string code, IEnumerable<TagViewModel> tags)
            : base(message, code, tags)
        {
        }

        public TagListResponse() : base() { }
    }
}
