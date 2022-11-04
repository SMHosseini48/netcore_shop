using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface IAddressService
{
    Task<ServiceResult> GetOneById(int id);
    Task<ServiceResult> Create(AddressCreateDto addressCreateDto);

    Task<ServiceResult> Update(AddressUpdateDto addressUpdateDto);

    Task<ServiceResult> Delete(int id);
}