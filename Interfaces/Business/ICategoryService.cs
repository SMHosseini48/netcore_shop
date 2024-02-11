using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface ICategoryService
{
    Task<ServiceResult> GetAll();
    Task<ServiceResult> Create(CategoryCreateDTO categoryCreateDto);
}