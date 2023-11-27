using ManagerProject.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManagerApp.Filters;

/// <summary>
/// Action filter for model validation
/// </summary>
public class ModelValidationActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var c = context.Controller as PersonsController;
        if (!c.ModelState.IsValid)
        {
            context.Result = c.RedirectToAction(nameof(c.ShowAll));
        }
    }
}
