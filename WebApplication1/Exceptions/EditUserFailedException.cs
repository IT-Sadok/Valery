namespace WebApplication1.Exceptions
{
    public class EditUserFailedException : Exception
    {
        public  EditUserFailedException(string error) : base(error)
        {

        }
    }
}
