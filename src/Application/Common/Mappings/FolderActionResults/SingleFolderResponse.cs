using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FolderResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de uma única pasta.
    /// </summary>
    public class SingleFolderResponse : SingleResponse<FolderViewModel>
    {
        public SingleFolderResponse(string message, string code, FolderViewModel folder)
            : base(message, code, folder)
        {
        }

        public SingleFolderResponse() : base() { }
    }
}
