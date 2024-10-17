using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Exceptions;

namespace WebApplication1
{
    public class UserService(UserManager<User> userManager, IJwtService jwtService) : IUserService
    {
        public async Task <AuthModel> RegisterUserAsync(CreateUserModel model)
        {
            var result = await userManager.CreateAsync(new User
            {
                Account = new Account
                {
                    CreatedAt = DateTime.UtcNow,
                    Description = model.AccountDescription
                },
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);

            if (!result.Succeeded)
            {
                throw new SignUpFailedException(string.Join(", ",
                    result.Errors.Select(x => x.Description)));
            }
            return new AuthModel
            {
                AccessToken = jwtService.GenerateJwt(model.UserName)
            };
        }
    }

    public interface IUserService
    {
        Task <AuthModel> RegisterUserAsync(CreateUserModel model);
    }
}
