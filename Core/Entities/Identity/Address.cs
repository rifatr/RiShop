using System.ComponentModel.DataAnnotations;
using Core.Identity.User;

namespace Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? House { get; set; }
        public string? Road { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}