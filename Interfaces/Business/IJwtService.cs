using System.Threading.Tasks;
using ncorep.Dtos;
using ncorep.Models;

namespace ncorep.Interfaces.Business;

public interface IJwtService
{
    public Task<ServiceResult> CreateToken(AppUser user);

    public Task<ServiceResult> RefreshToken(RefreshRequest refreshRequest);

}