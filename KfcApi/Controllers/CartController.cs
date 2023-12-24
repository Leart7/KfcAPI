using AutoMapper;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ICartRepository cartRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts(string? userId)
        {
            var cartItems = await _cartRepository.GetCartItems(userId);
            return Ok(_mapper.Map<List<CartDto>>(cartItems));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartRequestDto cartRequestDto)
        {
            var cartDomainModel = _mapper.Map<Cart>(cartRequestDto);
            var newCartItem = await _cartRepository.InsertIntoCart(cartDomainModel);
            return Ok(_mapper.Map<CartDto>(newCartItem));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] UpdateCartRequestDto cartRequestDto)
        {
            var cartDomainModel = _mapper.Map<Cart>(cartRequestDto);
            cartDomainModel = await _cartRepository.UpdateCartItem(id, cartDomainModel);
            if(cartDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CartDto>(cartDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int id)
        {
            var deletedCartItem = await _cartRepository.DeleteCartItem(id);
            if(deletedCartItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartDto>(deletedCartItem));
        }

        [HttpDelete]
        [Route("delete-all")]
        public async Task<IActionResult> DeleteAllCartItems(string? userId)
        {
            var deletedCartItems = await _cartRepository.DeleteCartItems(userId);
            return Ok(_mapper.Map<List<CartDto>>(deletedCartItems));
        }

    }
}
