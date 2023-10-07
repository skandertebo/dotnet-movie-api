namespace TP1.ReponseExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message) { }
    }
}
