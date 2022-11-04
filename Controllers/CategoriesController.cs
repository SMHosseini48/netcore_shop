using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[ApiController]
[Route("api/category")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetOneCategory(int id)
    {
        var result = await _categoryService.GetOne(id);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAll();
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddOne(CategoryCreateDTO categoryCreateDto)
    {
        var result =await _categoryService.Create(categoryCreateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}