using System.Reflection;
using Dev.Plugin.Auth.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dev.Plugin.Auth.Infrastructure.Data;

public class AuthDbContext : IdentityDbContext<DevUser>
{

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
         : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
