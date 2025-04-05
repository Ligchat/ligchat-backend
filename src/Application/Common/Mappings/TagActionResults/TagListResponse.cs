using System.Collections.Generic;

namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de tags.
    /// </summary>
    public class TagListResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public IEnumerable<TagViewModel> Data { get; set; }

        public TagListResponse()
        {
            Message = string.Empty;
            Code = string.Empty;
            Data = new List<TagViewModel>();
        }

        public TagListResponse(string message, string code, IEnumerable<TagViewModel> data)
        {
            Message = message;
            Code = code;
            Data = data;
        }
    }
}
