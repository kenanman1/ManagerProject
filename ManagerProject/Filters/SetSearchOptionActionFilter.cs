using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.DTO;
using ManagerApp.Controllers;

namespace ManagerApp.Filters;

/// <summary>
/// Action filter for setting search options in the ViewBag
/// </summary>
public class SetSearchOptionActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var con = context.Controller as PersonsController;
        var searchFilter = context.ActionArguments["searchFilter"];
        var searchText = context.ActionArguments["searchText"];

        if (searchText != null)
        {
            con.ViewBag.SearchText = searchText;
        }

        switch (searchFilter)
        {
            case nameof(PersonResponce.Name):
                con.ViewBag.Search = new List<SelectListItem>() { new SelectListItem("Person name", "Name", true), new SelectListItem("Surname", "Surname"), new SelectListItem("Email", "Email"), new SelectListItem("Address", "Address") };
                break;
            case nameof(PersonResponce.Surname):
                con.ViewBag.Search = new List<SelectListItem>() { new SelectListItem("Person name", "Name"), new SelectListItem("Surname", "Surname", true), new SelectListItem("Email", "Email"), new SelectListItem("Address", "Address") };
                break;
            case nameof(PersonResponce.Email):
                con.ViewBag.Search = new List<SelectListItem>() { new SelectListItem("Name", "Name"), new SelectListItem("Surname", "Surname"), new SelectListItem("Email", "Email", true), new SelectListItem("Address", "Address") };
                break;
            case nameof(PersonResponce.Address):
                con.ViewBag.Search = new List<SelectListItem>() { new SelectListItem("Name", "Name"), new SelectListItem("Surname", "Surname"), new SelectListItem("Email", "Email"), new SelectListItem("Address", "Address", true) };
                break;
            default:
                con.ViewBag.Search = new List<SelectListItem>() { new SelectListItem("Name", "Name"), new SelectListItem("Surname", "Surname"), new SelectListItem("Email", "Email"), new SelectListItem("Address", "Address") };
                break;
        }
    }
}
