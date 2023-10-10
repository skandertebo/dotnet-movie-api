using TP1.ResponseExceptions;

namespace TP1.ReponseExceptions
{
    public class NotFoundException : BaseResponseException
    {
        public NotFoundException(string message): base(message, TP1.ResponseExceptions.StatusCodes.NOT_FOUND) { }
    }
}
