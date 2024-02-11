using System.Collections.Generic;
using System.Linq;
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

    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Create(ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        product.ProductCategories = new List<ProductCategory>();
        foreach (var item in productCreateDto.CategoriesId)
            product.ProductCategories.Add(new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = item
            });

        await _unitOfWork.Products.InsertAsync(product);
        await _unitOfWork.Products.SaveChanges();

        var productDto = _mapper.Map<ProductDto>(product);
        return new ServiceResult {Data = productDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Update(ProductUpdateDTO productUpdateDto)
    {
        var product = await _unitOfWork.Products.GetOneByQueryAsync(q => q.Id == productUpdateDto.Id);
        if (product == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};
        _mapper.Map(productUpdateDto, product);
        _unitOfWork.Products.Update(product);
        var productCategories = await _unitOfWork.ProductCategories.GetAllAsync(x => x.ProductId == product.Id);
        _unitOfWork.ProductCategories.DeleteRange(productCategories);
        product.ProductCategories = new List<ProductCategory>();
        foreach (var item in productUpdateDto.CategoriesId)
            product.ProductCategories.Add(new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = item
            });

        _unitOfWork.Products.Update(product);
        await _unitOfWork.Products.SaveChanges();
        var productDto = _mapper.Map<ProductDto>(product);
        return new ServiceResult {Data = productDto, StatusCode = 200};
    }

    public async Task<ServiceResult> GetProductList(GetProductList getProductList)
    {
        var productList = await _unitOfWork.Products.GetAllAsync(x =>
            x.ProductCategories.Any(s => getProductList.CategoriesId.Any(z => z.Equals(s.CategoryId))));
        if (productList == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};
        var productListDto = _mapper.Map<List<ProductDto>>(productList);
        return new ServiceResult {Data = productListDto, StatusCode = 200};
    }
}