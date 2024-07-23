using Microsoft.AspNetCore.Mvc;

namespace Dev.Web.Components;

public class HeaderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
