using WebApplication1.Models.Enums;

namespace WebApplication1.DTO
{
    public class UpdateProfileModel
    {
        public int? Age { get; set; }
        public ProgrammerLevel Level { get; set; }
        public TechStack Stack { get; set; }
    }
}
