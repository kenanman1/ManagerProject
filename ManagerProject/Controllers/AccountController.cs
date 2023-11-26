using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ManagerApp.Models;

namespace ManagerApp.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    private RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize("NotAuthorised")]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize("NotAuthorised")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registration(RegisterViewModel register)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser user = new ApplicationUser { Email = register.Email, UserName = register.Email, PersonName = register.Name };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                List<string> errors = new();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                ViewBag.Errors = errors;

                return View();
            }
            await CreateRoles();
            if (register.Email == "admin@admin.com")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(PersonController.Index), "Person");
        }
        else
            return View();
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize("NotAuthorised")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize("NotAuthorised")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login, string? ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(await _userManager.FindByEmailAsync(login.Email), "Admin"))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    return LocalRedirect(ReturnUrl);

                return RedirectToAction(nameof(PersonController.Index), "Person");
            }
            else
                ModelState.AddModelError("Email", "Incorrect email or password.");
        }
        return View(login);
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(PersonController.Index), "Person");
    }

    [HttpGet]
    public async Task<IActionResult> CheckEmail(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Json(true);
        else
            return Json(false);
    }

    public async Task CreateRoles()
    {
        if (await _roleManager.FindByNameAsync("Admin") == null)
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        if (await _roleManager.FindByNameAsync("User") == null)
            await _roleManager.CreateAsync(new IdentityRole("User"));
    }
}
