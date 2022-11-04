using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CustomerPanel()
    {
        var result = await _customerService.CustomerProfile();
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpPut]
    [Route("profileupdate")]
    [Authorize]
    public async Task<IActionResult> UpdateCustomerProfile([FromBody] CustomerUpdateDto customerUpdateDto)
    {
        var result = await _customerService.ProfileUpdate(customerUpdateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
        return StatusCode(result.StatusCode, result.Data);
    }
}