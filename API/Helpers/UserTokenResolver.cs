using API.Dtos;
using AutoMapper;
using Core.Identity.User;
using Core.Interfaces;

namespace API.Helpers
{
    public class UserTokenResolver(ITokenService tokenService) : IValueResolver<AppUser, UserDto, string>
    {
        private readonly ITokenService _tokenService = tokenService;

        public string Resolve(AppUser source, UserDto destination, string destMember, ResolutionContext context)
        {
            return _tokenService.CreateToken(source);
        }
    }
}