using System.Net;

namespace IAW_API.Models.Responses.Common
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string> Messages { get; set; } = new();
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public void Success(T? value, string? customMessage = null)
        {
            IsSuccess = true;
            Data = value;
            Message = customMessage;
            StatusCode = HttpStatusCode.OK;
        }
        public void BadRequest(string? error)
        {
            IsSuccess = false;
            Message = error;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
