using Core.Identity.User;

namespace Core.Interfaces;

public interface ITokenService
{
    public string CreateToken(AppUser user);
}
