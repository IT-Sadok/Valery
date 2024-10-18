using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class UserService(UserManager<User> userManager, IJwtService jwtService, ApplicationContext context) : IUserService
    {
        public async Task <AuthModel> RegisterUserAsync(SignUpModel model)
        {
            var user = new User
            {
                Account = new Account
                {
                    CreatedAt = DateTime.UtcNow,
                    Description = model.AccountDescription
                },
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new SignUpFailedException(string.Join(", ",
                    result.Errors.Select(x => x.Description)));
            }
            return new AuthModel
            {
                AccessToken = jwtService.GenerateJwt(user.Id, user.UserName)
            };
        }
        public async Task<AuthModel> LoginUserAsync(SignInModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
            if(user == null)
            {
                throw new SignInFailedException("User not found");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user!, model.Password);

            if(!isPasswordValid)
            {
                throw new SignInFailedException("User not found");
            }
            return new AuthModel
            {
                AccessToken = jwtService.GenerateJwt(user.Id, user!.UserName!)
            };
        }
    }

    public interface IUserService
    {
        Task <AuthModel> RegisterUserAsync(SignUpModel model);
        Task<AuthModel> LoginUserAsync(SignInModel model);
    }
}
