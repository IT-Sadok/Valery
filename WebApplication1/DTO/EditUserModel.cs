using WebApplication1.Models.Enums;

namespace WebApplication1.DTO
{
    public class EditUserModel
    {
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string? AccountDescription { get; set; }
        public int? Age { get; set; }
        public ProgrammerLevel Level { get; set; }
        public TechStack Stack { get; set; }
    }
}
