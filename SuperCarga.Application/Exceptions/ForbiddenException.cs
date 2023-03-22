namespace SuperCarga.Application.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException() : base()
        {
        }

        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
