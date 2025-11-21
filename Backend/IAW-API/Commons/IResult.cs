using System.Net;

namespace IAW_API.Commons
{
    public interface IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Messages { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
