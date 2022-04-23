namespace Web.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message)
            : base(message)
        {

        }
        public ForbiddenAccessException() : base() { }
    }
}
