using System.Threading.Tasks;
using ncorep.Dtos;
using ncorep.Models;

namespace ncorep.Interfaces.Business;

public interface ICustomerService
{
    Task<ServiceResult> Create(int userId, UserRegisterDto userRegisterDto);
    Task<ServiceResult> CustomerProfile();
    Task<ServiceResult> ProfileUpdate(CustomerUpdateDto customerUpdateDto);
}