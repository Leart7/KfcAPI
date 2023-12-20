using AutoMapper;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderUserController : ControllerBase
    {
        private readonly IOrderUserRepository _orderUserRepository;
        private readonly IMapper _mapper;

        public OrderUserController(IOrderUserRepository orderUserRepository, IMapper mapper)
        {
            _orderUserRepository = orderUserRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderUser([FromBody] OrderUserRequestDto orderUserRequestDto)
        {
            var orderUserDomainModel = _mapper.Map<OrderUser>(orderUserRequestDto);
            var newOrderUser = await _orderUserRepository.CreateOrderUser(orderUserDomainModel);
            return Ok(_mapper.Map<OrderUserDto>(newOrderUser));
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetLastOrderUser([FromRoute] string userId)
        {
            var lastOrderUser = await _orderUserRepository.GetLastOrderUser(userId);
            if(lastOrderUser == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderUserDto>(lastOrderUser));
        }
    }
}
