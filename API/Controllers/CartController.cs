using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController (ICartRepository cartRepository, IMapper mapper) : BaseApiController
    {
        private readonly ICartRepository _cartRespository = cartRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string cartId)
        {
            var cart = await _cartRespository.GetCartAsync(cartId);

            return cart == null ? NotFound(new ApiResponse(404)) : Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCartDto customerCart)
        {
            return Ok(await _cartRespository.CreateOrUpdateCartAsync(_mapper.Map<CustomerCartDto, CustomerCart>(customerCart)));
        }

        [HttpDelete]
        public async Task DeleteCartById(string cartId)
        {
            await _cartRespository.DeleteCartAsync(cartId);
        }
    }
}