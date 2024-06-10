using Microsoft.AspNetCore.Identity;

namespace EventManagementAuthAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
