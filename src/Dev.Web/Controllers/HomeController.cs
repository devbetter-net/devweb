using Microsoft.AspNetCore.Mvc;

namespace Dev.Web.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

