using System.Security.Claims;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Identity.User;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(
    UserManager<AppUser> userManager, 
    SignInManager<AppUser> signInManager,
    ITokenService tokenService,
    IMapper mapper
) : BaseApiController
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindUserByEmailClaimsPrinciple(User); 
        if(user == null)
            return BadRequest(new ApiResponse(400, "No user found"));

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckIfEmailExistsAlrady([FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if(user == null) 
            return Unauthorized(new ApiResponse(401));

        var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if(!checkPassword.Succeeded)
            return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("signup")]
    public async Task<ActionResult<UserDto>> SingUp(SignUpDto signUpDto)
    {
        var user = new AppUser
        {
            DisplayName = signUpDto.DisplayName,
            Email = signUpDto.Email,
            UserName = signUpDto.Email
        };

        var result = await _userManager.CreateAsync(user, signUpDto.Password);

        if(!result.Succeeded)
            return BadRequest(new ApiResponse(400));

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpGet("currentuseraddress")]
    [Authorize]
    public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
    {
        var user = await _userManager.FindUserByEmailClaimsPrincipleWithAddress(User);

        if(user == null)
            return NotFound(new ApiResponse(404, "No user found"));
        if(user.Address == null)
            return NotFound(new ApiResponse(404, "No address found"));

        return _mapper.Map<Address, AddressDto>(user.Address);
    }

    [HttpPut("updateaddress")]
    [Authorize]
    public async Task<ActionResult<AddressDto>> UpdateAddressOfCurrentUser(AddressDto addressDto)
    {
        var user = await _userManager.FindUserByEmailClaimsPrincipleWithAddress(User);
        if(user == null)
            return NotFound(new ApiResponse(404, "No user found"));

        user.Address = _mapper.Map<AddressDto, Address>(addressDto);
        var result = await _userManager.UpdateAsync(user);

        if(result.Succeeded) return _mapper.Map<Address, AddressDto>(user.Address);

        return BadRequest(new ApiResponse(400, "Address update failed"));
    }
}
