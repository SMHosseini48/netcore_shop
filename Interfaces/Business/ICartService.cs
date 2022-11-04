using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface ICartService
{
    Task<ServiceResult> GetCart(int customerId);

    Task<ServiceResult> AddToCart(ShoppingCartRecordCreateDto item);

    Task<ServiceResult> DeleteItem(int itemId);

    Task<ServiceResult> UpdateCartItem(ShoppingCartRecordUpdateDto itemUpdate);
    
    Task<ServiceResult> DeleteCart(int customerId);
}