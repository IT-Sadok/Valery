using WebApplication1.Models.Enums;

namespace WebApplication1.DTO
{
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AccountDescription { get; set; }
        public int? Age { get; set; }
        public ProgrammerLevel Level { get;set ;}
        public TechStack Stack { get; set; }

    }
}
