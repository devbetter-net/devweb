namespace Dev.Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureWebApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
    }
}
