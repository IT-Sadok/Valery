
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserContext
    {
        public string? UserId { get; set; }

        public UserContext(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

        }
    }
}
