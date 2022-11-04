using System.Threading.Tasks;
using AutoMapper;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class ImageService : IImageService
{
    private readonly IGenericRepository<Image> _imageRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ImageService(IGenericRepository<Image> imageRepository,IGenericRepository<Product> productRepository
        ,IMapper mapper)
    {
        _imageRepository = imageRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Add(ImageCreateDto imageCreateDto)
    {
        var product = await _productRepository.GetOneByQueryAsync(q => q.Id == imageCreateDto.ProductId);
        if (product == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};
        var image = _mapper.Map<Image>(imageCreateDto);
        await _imageRepository.InsertAsync(image);
        image.FilePath = $"Images/{image.FileName}";
        await _imageRepository.SaveChanges();
        
        var imageDto = _mapper.Map<ImageDto>(image);
        return new ServiceResult {Data = imageDto, StatusCode = 200};
    }

    public async Task<ServiceResult> GetOne(int id)
    {
        var image = await _imageRepository.GetOneByQueryAsync(q => q.Id == id);
        if (image == null) return new ServiceResult {ErrorMessage = "image not found", StatusCode = 404};
        var imageDto = _mapper.Map<ImageDto>(image);
        return new ServiceResult {Data = imageDto, StatusCode = 200};
    }
}