using System.Threading.Tasks;
using ncorep.Dtos;

namespace ncorep.Interfaces.Business;

public interface IImageService
{
    Task<ServiceResult> Add(ImageCreateDto imageCreateDto);

    Task<ServiceResult> GetOne(int id);
}