using Core.Entities.Identity;
using Core.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Infra.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Hero Alom",
                    Email = "hero@net.com",
                    UserName = "heroalom",
                    Address = new Address
                    {
                        FirstName = "Hero",
                        LastName = "Alom",
                        House = "101",
                        Road = "2",
                        City = "Dhaka",
                        Country = "Bangladesh"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}