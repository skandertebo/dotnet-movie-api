namespace TP1.ResponseExceptions
{
    public class UnauthorizedException : BaseResponseException
    {
        public UnauthorizedException(string message) : base(message, TP1.ResponseExceptions.StatusCodes.UNAUTHORIZED) { }
    }
}
