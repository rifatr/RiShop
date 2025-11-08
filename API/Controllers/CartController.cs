using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController (ICartRepository cartRepository) : BaseApiController
    {
        private readonly ICartRepository _cartRespository = cartRepository;

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string cartId)
        {
            var cart = _cartRespository.GetCartAsync(cartId);

            return cart == null ? NotFound(new ApiResponse(404)) : Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCart customerCart)
        {
            return Ok(await _cartRespository.CreateOrUpdateCartAsync(customerCart));
        }

        [HttpDelete]
        public async Task DeleteCartById(string cartId)
        {
            await _cartRespository.DeleteCartAsync(cartId);
        }
    }
}