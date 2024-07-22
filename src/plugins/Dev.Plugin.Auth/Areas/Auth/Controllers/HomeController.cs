using Microsoft.AspNetCore.Mvc;

namespace Dev.Plugin.Auth.Areas.Auth.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
