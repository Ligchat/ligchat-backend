namespace LigChat.Backend.Application.Common.Models
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public T Data { get; set; }

        public ServiceResponse(string message, string code, T data)
        {
            Message = message;
            Code = code;
            Data = data;
        }

        public ServiceResponse()
        {
            Message = string.Empty;
            Code = string.Empty;
        }
    }
} 