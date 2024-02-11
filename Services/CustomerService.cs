using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;

namespace ncorep.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;


    public CustomerService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<ServiceResult> CustomerProfile()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();

        var user = await _unitOfWork.Users.GetOneByQueryAsync(x => x.Id == userId);
        var userDto = _mapper.Map<UserProfileDto>(user);

        return new ServiceResult {Data = userDto, StatusCode = 200};
    }
}