namespace WebApplication1.Exceptions
{
    public class SignUpFailedException : Exception
    {
        public SignUpFailedException(string error) : base(error)
        {
        }
    }
}
