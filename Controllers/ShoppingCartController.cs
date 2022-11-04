using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;

namespace ncorep.Controllers;

[ApiController]
[Route("api/shoppingcart")]
public class ShoppingCartController : ControllerBase
{
   private readonly ICartService _cartService;

   public ShoppingCartController(ICartService cartService)
   {
      _cartService = cartService;
   }

   [HttpGet]
   public async Task<IActionResult> GetCart(int customerId)
   {
      var result = await _cartService.GetCart(customerId);
      if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
      return StatusCode(result.StatusCode, result.Data);
   }

   [HttpPost]
   [Route("add")]
   public async Task<IActionResult> AddItem([FromBody] ShoppingCartRecordCreateDto shoppingCartRecordCreateDto)
   {
      var result = await _cartService.AddToCart(shoppingCartRecordCreateDto);
      if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
      return StatusCode(result.StatusCode, result.Data);
   }

   [HttpDelete]
   [Route("{id:int}")]
   public async Task<IActionResult> DeletItem([FromBody] int id)
   {
      var result = await _cartService.DeleteItem(id);
      if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
      return StatusCode(result.StatusCode, result.Data);
   }

   [HttpPut]
   [Route("updateitem")]
   public async Task<IActionResult> UpdateCartItem([FromBody] ShoppingCartRecordUpdateDto shoppingCartRecordUpdateDto)
   {
      var result = await _cartService.UpdateCartItem(shoppingCartRecordUpdateDto);
      if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
      return StatusCode(result.StatusCode, result.Data);
   }

   [HttpDelete]
   [Route("deletecart/{id:int}")]
   public async Task<IActionResult> DeleteCart(int customerId)
   {
      var result = await _cartService.DeleteCart(customerId);
      if (result.Data == null) return StatusCode(result.StatusCode, result.ErrorMessage);
      return StatusCode(result.StatusCode, result.Data);
   }
   
}