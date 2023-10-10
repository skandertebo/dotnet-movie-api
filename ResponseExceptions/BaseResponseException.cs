namespace TP1.ResponseExceptions
{
    public enum StatusCodes
    {
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        NOT_FOUND = 404
    }
    public class BaseResponseException : Exception
    {
        public StatusCodes StatusCode { get; set; }
        public BaseResponseException(string message, StatusCodes statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
