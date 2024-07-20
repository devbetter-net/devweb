using Microsoft.AspNetCore.Mvc;

namespace Dev.Plugin.Blog.Areas.Blog.Controllers;
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
