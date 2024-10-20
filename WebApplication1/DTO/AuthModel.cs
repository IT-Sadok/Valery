namespace WebApplication1.DTO
{
    public class AuthModel
    {
        public string AccessToken { get; set; }
        public bool? IsAuthenticated { get; set; }
        public string? Message { get; set; }
    }
}
