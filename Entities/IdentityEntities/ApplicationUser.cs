using Microsoft.AspNetCore.Identity;

namespace Entities.IdentityEntities;

public class ApplicationUser : IdentityUser
{
    public string PersonName { get; set; }
    public int TimesEdited { get; set; }
    public int TimesCreated { get; set; }
    public int TimesDeleted { get; set; }
}
