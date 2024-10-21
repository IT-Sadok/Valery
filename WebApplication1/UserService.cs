using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

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
                    Description = model.AccountDescription,
                    Age = model.Age,
                    Level = model.Level,
                    Stack = model.Stack
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

        public async Task<bool> UpdateProfileAsync(string userId, UpdateProfileModel model)
        {
            var user = await context.Users
            .Include(u => u.Account)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                throw new UpdateProfileFailedException("User not found");
            }

            user.Account.Age = model.Age;
            user.Account.Stack = model.Stack;
            user.Account.Level = model.Level;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new UpdateProfileFailedException(string.Join(", ",
                    result.Errors.Select(x => x.Description)));
            }
            return true;
        }

        public async Task<bool> ChangeEmailAsync(string userId, ChangeEmailModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UpdateEmailFailedException("User not found");
            }

            user.Email = model.NewEmail;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new UpdateEmailFailedException(string.Join(", ",
                    result.Errors.Select(x => x.Description)));
            }
            return true;
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            var isPasswordValid = await userManager.CheckPasswordAsync(user!, model.CurrentPassword);

            if (user == null || !isPasswordValid)
            {
                throw new UpdatePasswordFailedException("User not found or password is incorrect");
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new UpdatePasswordFailedException(string.Join(", ",
                    result.Errors.Select(x => x.Description)));
            }
            return true;
        }
    }

    public interface IUserService
    {
        Task <AuthModel> RegisterUserAsync(SignUpModel model);
        Task<AuthModel> LoginUserAsync(SignInModel model);
        Task<bool> UpdateProfileAsync(string userId, UpdateProfileModel model);
        Task<bool> ChangeEmailAsync(string userId, ChangeEmailModel model);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordModel model);
    }
}
