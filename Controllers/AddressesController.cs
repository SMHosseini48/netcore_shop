using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[Route("api/address")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserAddresses()
    {
        var result = await _addressService.GetUserAddresses();
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddressCreateDto addressCreateDto)
    {
        var result = await _addressService.Create(addressCreateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [Route("update")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AddressUpdateDto addressUpdateDto)
    {
        var result = await _addressService.Update(addressUpdateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        var result = await _addressService.Delete(id);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}