using Microsoft.AspNetCore.Identity;

namespace ProductApi.Entities.Auth
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
