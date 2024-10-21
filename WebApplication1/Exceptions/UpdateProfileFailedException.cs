namespace WebApplication1.Exceptions
{
    public class UpdateProfileFailedException : Exception
    {
        public UpdateProfileFailedException(string error) : base(error) { }
    }
}
