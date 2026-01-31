using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity.User
{
    public class AppUser : IdentityUser
    {
        public required string DisplayName { get; set; }
        public Address? Address { get; set; }
    }
}