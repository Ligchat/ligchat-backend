using LigChat.Backend.Application.Utilities;

namespace LigChat.Backend.Application.Common.Mappings.SectorActionResults
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único setor.
    /// </summary>
    public class SingleSectorResponse : SingleResponse<SectorViewModel>
    {
        public SingleSectorResponse(string message, string code, SectorViewModel sector)
            : base(message, code, sector)
        {
        }

        public SingleSectorResponse() : base() { }
    }
}
