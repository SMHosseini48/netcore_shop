using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[Route("api/order")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize]
    [Route("add")]
    public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        var result = await _orderService.OrderRegister(orderCreateDto);
        if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);

        return StatusCode(result.StatusCode, result.Data);
    }
}