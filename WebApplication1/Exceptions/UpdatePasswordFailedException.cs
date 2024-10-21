namespace WebApplication1.Exceptions
{
    public class UpdatePasswordFailedException : Exception
    {
        public UpdatePasswordFailedException(string error) : base(error) { }
    }
}
