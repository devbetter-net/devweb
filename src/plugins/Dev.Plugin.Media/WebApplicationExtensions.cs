using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.Plugin.Media;

public static class WebApplicationExtensions
{
    public static void AddDevMedia(this IServiceCollection services, WebApplicationBuilder builder)
    {
        //database
        //services
    }

    public static void UseDevMedia(this WebApplication app)
    {
    }
}
