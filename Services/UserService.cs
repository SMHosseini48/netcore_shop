using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IGenericRepository<Customer> customerRepository,
        IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _customerRepository = customerRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResult> Register(UserRegisterDto userRegisterDto)
    {
        var userExist = await _userManager.FindByEmailAsync(userRegisterDto.Email);
        if (userExist != null) return new ServiceResult {ErrorMessage = "user already exist", StatusCode = 403};

        var user = _mapper.Map<AppUser>(userRegisterDto);
        user.UserName = userRegisterDto.Email;
        await _userManager.UpdateAsync(user);
        await _userManager.CreateAsync(user, userRegisterDto.Password);
        return new ServiceResult {Data = user, StatusCode = 200};
    }

    public async Task<ServiceResult> Login(UserLoginDTO userLoginDto)
    {
        var signedIn =
            await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, false, false);
        if (!signedIn.Succeeded) return new ServiceResult {ErrorMessage = "incorrect credentials", StatusCode = 401};
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        var userLoginResponseDto = _mapper.Map<UserLoginResponseDTO>(user);
        return new ServiceResult {Data = userLoginResponseDto, StatusCode = 200};
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ServiceResult> Find()
    {
        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
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

    public async Task<ServiceResult> ChangeEmail(string newEmail)
    {
        var userExist = await _userManager.FindByEmailAsync(newEmail);
        if (userExist != null) return new ServiceResult {ErrorMessage = "email already taken", StatusCode = 409};

        var username = _httpContextAccessor.HttpContext.User.Identity.Name;
        if (username == null) return new ServiceResult {ErrorMessage = "user not logged in", StatusCode = 404};
        var user = await _userManager.FindByEmailAsync(username);
        user.UserName = user.Email = newEmail;
        var customer = await _customerRepository.GetOneByQueryAsync(q => q.AppUserId == user.Id);
        customer.Email = newEmail;
        _customerRepository.Update(customer);
        await _userManager.UpdateAsync(user);
        await _customerRepository.SaveChanges();
        return new ServiceResult {Data = newEmail, StatusCode = 200};
    }
    // public async Task<IdentityResult> AddToRole(AppUser user, IdentityRole role)
    // {
    //     return await _userManager.AddToRoleAsync(user, role.Name);
    // }
}