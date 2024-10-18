namespace WebApplication1.Exceptions
{
    public class SignInFailedException : Exception
    {
        public SignInFailedException(string error) : base(error)
        {
        }
    }
}
