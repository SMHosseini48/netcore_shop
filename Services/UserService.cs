using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<UserService> _logger;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger, IJwtService jwtService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<ServiceResult> Register(UserRegisterDto userRegisterDto)
    {
        var userExist = await _userManager.FindByEmailAsync(userRegisterDto.Email);
        if (userExist != null) return new ServiceResult {ErrorMessage = "user already exist", StatusCode = 409};

        var user = _mapper.Map<AppUser>(userRegisterDto);
        user.UserName = userRegisterDto.Email;
        await _userManager.CreateAsync(user, userRegisterDto.Password);
        return await _jwtService.CreateToken(user);
    }


    public async Task<ServiceResult> Login(UserLoginDTO userLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if (user == null) return new ServiceResult {ErrorMessage = "user not found", StatusCode = 404};
        var userHasValidPassword = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
        if (!userHasValidPassword)
            return new ServiceResult {ErrorMessage = "wrong user/password combination", StatusCode = 403};

        var result = await _jwtService.CreateToken(user);
        return new ServiceResult {Data = result, StatusCode = 200};
    }
    
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ServiceResult> Find()
    {
        var userName =
            _httpContextAccessor.HttpContext.User.Identity
                .Name; // do not capture httpcontext outside of the request flow 
        if (userName == null) return new ServiceResult {ErrorMessage = "user not logged in", StatusCode = 404};
        var user = await _userManager.FindByNameAsync(userName);
        return new ServiceResult {Data = user, StatusCode = 200};
    }

    public async Task<ServiceResult> ChangePassword(string currentPassword, string newPassword)
    {
        var username = _httpContextAccessor.HttpContext.User.Identity.Name;
        if (username == null) return new ServiceResult {ErrorMessage = "user not logged in", StatusCode = 404};
        var user = await _userManager.FindByEmailAsync(username);
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded) return new ServiceResult {ErrorMessage = "wrong password", StatusCode = 401};
        return new ServiceResult {Data = result, StatusCode = 200};
    }

    // public async Task<ServiceResult> ChangeEmail(string newEmail)
    // {
    //     var userExist = await _userManager.FindByEmailAsync(newEmail);
    //     if (userExist != null) return new ServiceResult {ErrorMessage = "email already taken", StatusCode = 409};
    //
    //     var username = _httpContextAccessor.HttpContext.User.Identity.Name;
    //     if (username == null) return new ServiceResult {ErrorMessage = "user not logged in", StatusCode = 404};
    //     var user = await _userManager.FindByEmailAsync(username);
    //     user.UserName = user.Email = newEmail;
    //     await _userManager.ChangeEmailAsync(user,
    // }
    // public async Task<IdentityResult> AddToRole(AppUser user, IdentityRole role)
    // {
    //     return await _userManager.AddToRoleAsync(user, role.Name);
    // }
}