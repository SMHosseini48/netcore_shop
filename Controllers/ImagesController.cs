using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[ApiController]
[Route("api/image")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageservice)
    {
        _imageService = imageservice;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _imageService.GetOne(id);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> CreateProductImage([FromBody] ImageCreateDto imageCreateDto)
    {
        var result = await _imageService.Add(imageCreateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}