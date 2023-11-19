using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ManagerProject.Areas.User.Models;

namespace ManagerProject.Areas.User.Controllers
{
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
        public IActionResult UserProfile()
        {
            var user = _userManager.FindByEmailAsync(User.Identity.Name);
            UserViewModel userViewModel = new UserViewModel { Email = user.Result.Email, PersonName = user.Result.PersonName };
            return View(userViewModel);
        }
    }
}
