using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface IOrderService
{
    Task<ServiceResult> OrderRegister(OrderCreateDto orderCreateDto);
}