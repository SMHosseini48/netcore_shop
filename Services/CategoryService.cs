using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class CategoryService : ICategoryService

{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResult> GetOne(int id)
    {
        var category = await _categoryRepository.GetOneByQueryAsync(q => q.Id == id,includes: new List<string> {"Products"});
        if (category == null) return new ServiceResult {ErrorMessage = "category not found", StatusCode = 404};

        var categoryDto = _mapper.Map<CategoryDTO>(category);
        return new ServiceResult {Data = categoryDto, StatusCode = 200};
    }

    public async Task<ServiceResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
        return new ServiceResult {Data = categoriesDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Create(CategoryCreateDTO categoryCreateDto)
    {
        var category = _mapper.Map<Category>(categoryCreateDto);
        await _categoryRepository.InsertAsync(category);
        await _categoryRepository.SaveChanges();
        
        var categoryDto = _mapper.Map<CategoryDTO>(category);
        return new ServiceResult {Data = categoryDto, StatusCode = 200};
    }
}