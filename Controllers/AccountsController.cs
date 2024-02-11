using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[Route("api/account")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public AccountsController(IUserService userService, IConfiguration configuration, IJwtService jwtService)
    {
        _configuration = configuration;
        _jwtService = jwtService;
        _userService = userService;
    }

    /// <summary>
    ///     registers a user
    /// </summary>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            ModelState.AddModelError(nameof(userRegisterDto.ConfirmPassword),
                "Password Confirmation does not match");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var result = await _userService.Register(userRegisterDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
    {
        var result = await _userService.Login(userLoginDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return Ok("logout successful");
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest refreshRequest)
    {
        var result = await _jwtService.RefreshToken(refreshRequest);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Route("test2")]
    public IActionResult test1()
    {
        var some = TimeSpan.Parse("00:05:10");
        var now = DateTime.Now;
        var nowandsome = now.Add(some);

        return Ok($"{some}   {now}   {nowandsome}");
    }

    // [HttpPost]
    // [Route("changeemail")]
    // [Authorize]
    // public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    // {
    //     var result = await _userService.ChangeEmail(changeEmailDto.NewEmail);
    //     if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
    //     return StatusCode(result.StatusCode, result.Data);
    // }

    // [HttpPost]
    // [Route("changepassword")]
    // [Authorize]
    // public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    // {
    //     if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
    //         ModelState.AddModelError(nameof(changePasswordDto.ConfirmNewPassword),
    //             "Password Confirmation does not match");
    //     if (!ModelState.IsValid) return ValidationProblem(ModelState);
    //
    //
    //     var result =
    //         await _userService.ChangePassword(changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
    //     if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
    //     return StatusCode(result.StatusCode, result.Data);
    // }
}