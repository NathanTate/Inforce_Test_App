namespace Inforce_Api.Models
{
    public class ApiException : Exception
    {
        public ApiException(int statusCode, string message, string details) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Details { get; set; }
    }
}
