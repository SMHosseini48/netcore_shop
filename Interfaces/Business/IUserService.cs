using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface IUserService
{
    Task<ServiceResult> Register(UserRegisterDto userRegisterDto);

    // Task<ServiceResult> Login(UserLoginDTO userLoginDto);
    Task Logout();

    Task<ServiceResult> Login(UserLoginDTO userLoginDto);
}