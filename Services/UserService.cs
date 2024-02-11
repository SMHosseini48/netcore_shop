using System.Linq;
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
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<AppUser> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtService jwtService,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = new PasswordHasher<AppUser>();
    }

    public async Task<ServiceResult> Register(UserRegisterDto userRegisterDto)
    {
        var userNormalizedEmail = userRegisterDto.Email.Normalize();
        var userExist = await _unitOfWork.Users.GetOneByQueryAsync(x => x.Email == userNormalizedEmail);
        if (userExist != null) return new ServiceResult {ErrorMessage = "user already exist", StatusCode = 409};

        var user = new AppUser
        {
            UserName = userRegisterDto.Email.Normalize(),
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userNormalizedEmail,
            IsAuthenticated = true
        };

        var hashedPassword = _passwordHasher.HashPassword(user, userRegisterDto.Password);
        user.PasswordHash = hashedPassword;
        await _unitOfWork.Users.InsertAsync(user);
        return await _jwtService.CreateToken(user);
    }


    public async Task<ServiceResult> Login(UserLoginDTO userLoginDto)
    {
        var user = await _unitOfWork.Users.GetOneByQueryAsync(x => x.Email == userLoginDto.Email);
        if (user == null) return new ServiceResult {ErrorMessage = "user not found", StatusCode = 404};

        var passwordComparisonResult =
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);

        if (passwordComparisonResult != PasswordVerificationResult.Success)
            return new ServiceResult {ErrorMessage = "wrong user/password combination", StatusCode = 403};

        var result = await _jwtService.CreateToken(user);
        user.IsAuthenticated = true;

        await _unitOfWork.Users.SaveChanges();
        return new ServiceResult {Data = result, StatusCode = 200};
    }

    public async Task Logout()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();
        var user = await _unitOfWork.Users.GetOneByQueryAsync(x => x.Id == userId);
        user.IsAuthenticated = false;
        await _unitOfWork.Users.SaveChanges();
    }
}