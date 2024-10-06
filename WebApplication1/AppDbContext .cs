using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>().HasData(
            new Person { Id = 1, Name = "Oscar", LastName = "Montenegro", Age = 28 },
            new Person { Id = 2, Name = "Yolanda", LastName = "Montenegro", Age = 27 }
        );

        base.OnModelCreating(builder);
    }

    public DbSet<Person> People { get; set; }
}