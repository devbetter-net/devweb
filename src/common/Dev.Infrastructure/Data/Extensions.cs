using Microsoft.EntityFrameworkCore;

namespace Dev.Infrastructure.Data;

public static class Extensions
{
    public static string GenerateCreateScript(this DbContext dbContext)
    {
        return dbContext.Database.GenerateCreateScript();
    }
}