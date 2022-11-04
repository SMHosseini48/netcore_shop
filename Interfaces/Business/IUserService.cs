using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ncorep.Dtos;
using ncorep.Models;

namespace ncorep.Interfaces.Business;

public interface IUserService
{
    Task<ServiceResult> Register(UserRegisterDto userRegisterDto);
    Task<ServiceResult> Login(UserLoginDTO userLoginDto);
    Task Logout();
    Task<ServiceResult> Find();
    Task<ServiceResult> ChangeEmail(string newEmail);
    Task<ServiceResult> ChangePassword(string currentPassword, string newPassword);
}