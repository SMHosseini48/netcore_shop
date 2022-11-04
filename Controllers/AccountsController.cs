using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Models;

namespace ncorep.Controllers;

[Route("api/account")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IUserService _userService;

    public AccountsController(IUserService userService, ICustomerService customerService)
    {
        _userService = userService;
        _customerService = customerService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            ModelState.AddModelError(nameof(userRegisterDto.ConfirmPassword),
                "Password Confirmation does not match");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var registerResult = await _userService.Register(userRegisterDto);
        if (registerResult.Data == null) return StatusCode(registerResult.StatusCode, registerResult.ErrorMessage);

        var result = await _customerService.Create(((AppUser) registerResult.Data).Id, userRegisterDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
    {
        var result = await _userService.Login(userLoginDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return Ok("logout successfull");
    }

    [HttpPost]
    [Route("changeemail")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        var result = await _userService.ChangeEmail(changeEmailDto.NewEmail);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPost]
    [Route("changepassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            ModelState.AddModelError(nameof(changePasswordDto.ConfirmNewPassword),
                "Password Confirmation does not match");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);


        var result =
            await _userService.ChangePassword(changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}