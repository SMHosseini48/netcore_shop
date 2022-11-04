using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Models;

namespace ncorep.Controllers;

[ApiController]
[Route("api/product")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    //[Authorize(Roles = nameof(Roles.Admin))]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
    {
        var result = await _productService.Create(
            productCreateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
        
    }
    
    //[Authorize(Roles = nameof(Roles.Admin))]
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] ProductUpdateDTO productUpdateDto)
    {
        var result = await _productService.Update(productUpdateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    //[Authorize(Roles = nameof(Roles.Admin))]
    [HttpGet]
    public async Task<IActionResult> GetOne(int id)
    {
        var result = await _productService.GetById(id);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}