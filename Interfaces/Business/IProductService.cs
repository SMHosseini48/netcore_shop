using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface IProductService
{
    Task<ServiceResult> Create(ProductCreateDto productCreateDto);

    Task<ServiceResult> Update(ProductUpdateDTO productUpdateDto);

    Task<ServiceResult> GetById(int id);
}