using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Models;

public class ApplicationContext : IdentityDbContext <User, IdentityRole<int>, int>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }   

    public DbSet<Account> Accounts => Set<Account>();
}