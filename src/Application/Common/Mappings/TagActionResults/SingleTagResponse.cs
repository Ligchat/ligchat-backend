using System;
using System.Collections.Generic;

namespace LigChat.Backend.Application.Common.Mappings.TagActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única tag.
    /// </summary>
    public class SingleTagResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public TagViewModel? Data { get; set; }

        public SingleTagResponse()
        {
            Message = string.Empty;
            Code = string.Empty;
            Data = null;
        }

        public SingleTagResponse(string message, string code, TagViewModel? data)
        {
            Message = message;
            Code = code;
            Data = data;
        }
    }
}
