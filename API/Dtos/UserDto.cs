namespace API.Dtos;

public class UserDto
{
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public required string Token { get; set; }
}
