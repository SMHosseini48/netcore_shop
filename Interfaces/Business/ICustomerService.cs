using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface ICustomerService
{
    Task<ServiceResult> CustomerProfile();
}