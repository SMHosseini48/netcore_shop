using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public ProductService(IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Create(ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        await _productRepository.InsertAsync(product);
        await _productRepository.SaveChanges();
        
        var productDto = _mapper.Map<ProductDto>(product);
        return new ServiceResult {Data = productDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Update(ProductUpdateDTO productUpdateDto)
    {
        var product = await _productRepository.GetOneByQueryAsync(q => q.Id == productUpdateDto.Id);
        if (product == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};
        _mapper.Map(productUpdateDto, product);
        _productRepository.Update(product);
        await _productRepository.SaveChanges();
        
        var productDto = _mapper.Map<ProductDto>(product);
        return new ServiceResult {Data = productDto, StatusCode = 200};
    }

    public async Task<ServiceResult> GetById(int id)
    {
        var product = await _productRepository.GetOneByQueryAsync(q => q.Id == id , includes: new List<string> {"Category"});
        if (product == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};
        var productDto = _mapper.Map<ProductDto>(product);
        return new ServiceResult {Data = productDto, StatusCode = 200};
    }
}