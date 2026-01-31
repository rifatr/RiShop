namespace API.Dtos;

public class SignUpDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string DisplayName { get; set; }
}
