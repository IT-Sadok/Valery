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

        public async Task<AuthModel> EditUserAsync(EditUserModel model)
        {
            //bottleneck
            var user = await context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.UserName == model.OldUserName);

            if (user!.Email != model.Email)
            {
                //change it to https://stackoverflow.com/questions/36367140/aspnet-core-generate-and-change-email-address
                var changeEmailToken = await userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                var emailResult = await userManager.ChangeEmailAsync(user, model.Email, changeEmailToken);
                if (!emailResult.Succeeded)
                {
                    throw new EditUserFailedException("Email change failed");
                }
            }

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var passwordResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    throw new EditUserFailedException("Password change failed");
                }
            }

            bool hasChanges = false;

            if (user.Account.Description != null && user.Account.Description != model.AccountDescription)
            {
                user.Account.Description = model.AccountDescription;
                hasChanges = true;
            }
            if (user.Account.Age != model.Age)
            {
                user.Account.Age = model.Age;
                hasChanges = true;
            }

            if (user.Account.Level != model.Level)
            {
                user.Account.Level = model.Level;
                hasChanges = true;
            }

            if (user.Account.Stack != model.Stack)
            {
                user.Account.Stack = model.Stack;
                hasChanges = true;
            }

            if (hasChanges)
            {
                var updateResult = await userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    throw new EditUserFailedException("User fields change failed");
                }
            }

            return new AuthModel { IsAuthenticated = true, Message = "User updated successfully" };
        }
    }

    public interface IUserService
    {
        Task <AuthModel> RegisterUserAsync(SignUpModel model);
        Task<AuthModel> LoginUserAsync(SignInModel model);
        Task<AuthModel> EditUserAsync(EditUserModel model);
    }
}
