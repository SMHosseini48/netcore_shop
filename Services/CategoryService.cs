using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class CategoryService : ICategoryService

{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService( IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<ServiceResult> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();

        if (categories.IsNullOrEmpty())
            return new ServiceResult {ErrorMessage = "no category registered", StatusCode = 404};
        var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
        return new ServiceResult {Data = categoriesDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Create(CategoryCreateDTO categoryCreateDto)
    {
        var category = _mapper.Map<Category>(categoryCreateDto);
        await _unitOfWork.Categories.InsertAsync(category);
        await _unitOfWork.Categories.SaveChanges();

        var categoryDto = _mapper.Map<CategoryDTO>(category);
        return new ServiceResult {Data = categoryDto, StatusCode = 200};
    }
}