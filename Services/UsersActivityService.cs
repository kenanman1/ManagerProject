using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public class UsersActivityService : IUsersActivityService
{
    private UserManager<ApplicationUser> _userManager;
    public UsersActivityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateTimesCreated(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        user.TimesCreated++;
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateTimesDeleted(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        user.TimesDeleted++;
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateTimesEdited(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        user.TimesEdited++;
        await _userManager.UpdateAsync(user);
    }
}
