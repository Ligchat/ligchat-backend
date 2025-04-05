namespace tests_.src.Domain.DTOs.SectorDto
{
    public class SectorExistenceResponse
    {
        public string Message { get; }
        public string StatusCode { get; }
        public bool Exists { get; }

        public SectorExistenceResponse(string message, string statusCode, bool exists)
        {
            Message = message;
            StatusCode = statusCode;
            Exists = exists;
        }
    }
}
