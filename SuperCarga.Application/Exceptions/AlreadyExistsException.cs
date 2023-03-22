namespace SuperCarga.Application.Exceptions
{
    public class AlreadyExistsException : ApplicationException
    {
        public AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
