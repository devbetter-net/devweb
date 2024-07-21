using Dev.Plugin.Auth.Core.Domain;
using Dev.Plugin.Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.Plugin.Auth;

public static class WebApplicationExtensions
{
    public static void AddDevAuthenticate(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        //services
        
        string connectionString = builder.Configuration.GetConnectionString("Auth")!;

        //mysql
        builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
        
        //Configure Authentication
        //1. Add identity service
        builder.Services.AddIdentity<DevUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        //2. Configure the application cookie
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/auth/Authentication/Login";
            options.SlidingExpiration = true;
        });

        //3. Update the default password settings
        builder.Services.Configure<IdentityOptions>(options =>
        {
            //Password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;

            //Lockout settings
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            //Signin settings
            options.SignIn.RequireConfirmedEmail = true;
        });
        services.AddAuthentication().AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration!["Authentication:Google:ClientId"];
            googleOptions.ClientSecret = configuration!["Authentication:Google:ClientSecret"];
        });
        

    }

    public static void UseDevAuthenticate(this WebApplication app)
    {
        app.UseAuthorization();
    }
}
