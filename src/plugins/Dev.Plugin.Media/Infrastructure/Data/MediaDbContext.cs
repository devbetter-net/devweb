using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Dev.Plugin.Media.Infrastructure.Data;

public class MediaDbContext : DbContext
{
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
