using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.SectorActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de setores.
    /// </summary>
    public class SectorListResponse : ListResponse<SectorViewModel>
    {
        public SectorListResponse(string message, string code, IEnumerable<SectorViewModel> sectors)
            : base(message, code, sectors)
        {
        }

        public SectorListResponse() : base() { }
    }
}
