using System.Collections.Generic;
using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface ICategoryService
{
    Task<ServiceResult> GetOne(int id);
    Task<ServiceResult> GetAll();
    Task<ServiceResult> Create(CategoryCreateDTO categoryCreateDto);
}