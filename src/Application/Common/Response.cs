namespace LigChat.Backend.Application.Common
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public Response()
        {
            Success = true;
            Message = string.Empty;
            StatusCode = 200;
        }

        public static Response<T> Ok(T data, string message = "Success")
        {
            return new Response<T>
            {
                Success = true,
                Message = message,
                StatusCode = 200,
                Data = data
            };
        }

        public static Response<T> Error(string message, int statusCode = 400)
        {
            return new Response<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                Data = default
            };
        }

        public static Response<T> NotFound(string message = "Resource not found")
        {
            return new Response<T>
            {
                Success = false,
                Message = message,
                StatusCode = 404,
                Data = default
            };
        }
    }
} 