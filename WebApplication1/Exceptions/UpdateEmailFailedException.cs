namespace WebApplication1.Exceptions
{
    public class UpdateEmailFailedException : Exception
    {
        public UpdateEmailFailedException(string error) : base(error) { }
    }
}
