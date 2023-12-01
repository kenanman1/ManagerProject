using Entities.IdentityEntities;
using ManagerProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace ManagerProject.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("[action]")]
    public async Task<IActionResult> ShowAllUsers()
    {
        return View(await _userManager.Users.ToListAsync());
    }

    [Route("[action]/{email}")]
    [HttpGet]
    public async Task<IActionResult> DeleteUser(string email)
    {
        ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        DeleteUser deleteUser = new DeleteUser { Email = user.Email, PersonName = user.PersonName };
        return View(deleteUser);
    }

    [Route("[action]/{email}")]
    [HttpPost]
    public async Task<IActionResult> DeleteUser(DeleteUser deleteUser)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(deleteUser.Email);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction(nameof(ShowAllUsers));
    }

    [Route("[action]")]
    public async Task<IActionResult> UsersToPdf()
    {
        var users = await _userManager.Users.ToListAsync();

        return new ViewAsPdf(users)
        {
            FileName = "AllUsers.pdf",
            PageSize = Size.A4,
            PageOrientation = Orientation.Portrait,
        };
    }
}
