using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class AddressDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? House { get; set; }
        [Required]
        public string? Road { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
    }
}