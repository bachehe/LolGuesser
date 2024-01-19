using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                AppUser user = new()
                {
                    DisplayName = "Admin",
                    Email = "admin@admin.com",
                    UserName = "Admin",
                    Address = new() { FirstName = "Damian", City = "Kraków" }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
