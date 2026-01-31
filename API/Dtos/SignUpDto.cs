using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class SignUpDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(
        pattern: "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){6,16}$", 
        ErrorMessage = "Password must contain one lowercase, one upper case, one special character, one neumeric and at least 6 and at most 16 characters."
    )]
    public string? Password { get; set; }
    [Required]
    public string? DisplayName { get; set; }
}
