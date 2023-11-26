using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ManagerApp.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    [Route("/Error")]
    public IActionResult Error()
    {
        if(HttpContext.Features.Get<IExceptionHandlerFeature>() != null)
        {
            return View();
        }
        else
        {
            return RedirectToAction(nameof(PersonController.Index), "Person");
        }
    }

    [Route("/Accessdenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
