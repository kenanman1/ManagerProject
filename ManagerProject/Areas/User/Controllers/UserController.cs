using Entities.IdentityEntities;
using ManagerProject.Areas.User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagerProject.Areas.User.Controllers;

[Route("[controller]")]
[Area("User")]
[Authorize]
public class UserController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("[action]")]
    public async Task<IActionResult> UserProfile()
    {
        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        UserViewModel userViewModel = new UserViewModel { Email = user.Email, PersonName = user.PersonName };
        return View(userViewModel);
    }
}
