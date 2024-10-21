using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTO;
using System.Text;
using WebApplication1;
using WebApplication1.Options;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgDbConnection"));
});

builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("sign-up", ([FromServices] IUserService userService,
    [FromBody] SignUpModel model) => userService.RegisterUserAsync(model));

app.MapPost("sign-in", ([FromServices] IUserService userService,
    [FromBody] SignInModel model) => userService.LoginUserAsync(model));

app.MapPatch("email", async ([FromServices] IUserService userService,
    [FromBody] ChangeEmailModel model) =>
{
    var result = await userService.ChangeEmailAsync(model);
    return result ? Results.Ok("Email updated successfully.") : Results.NotFound("User not found.");
}).RequireAuthorization();

app.MapPatch("password", async ([FromServices] IUserService userService,
    [FromBody] ChangePasswordModel model) =>
{
    var result = await userService.ChangePasswordAsync(model);
    return result ? Results.Ok("Password updated successfully.") : Results.BadRequest("Invalid current password or user not found.");
}).RequireAuthorization();

app.MapPut("me", async (
    [FromServices] IUserService userService,
    [FromBody] UpdateProfileModel model) =>
{
    var result = await userService.UpdateProfileAsync(model);
    return result ? Results.Ok("Profile updated successfully.") : Results.NotFound("User not found.");
}).RequireAuthorization();

app.Run();
