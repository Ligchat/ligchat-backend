using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.FolderResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de pastas.
    /// </summary>
    public class FolderListResponse : ListResponse<FolderViewModel>
    {
        public FolderListResponse(string message, string code, IEnumerable<FolderViewModel> folderList)
            : base(message, code, folderList)
        {
        }

        public FolderListResponse() : base() { }
    }
}
