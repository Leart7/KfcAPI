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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            var orderDomainModel = _mapper.Map<Order>(orderRequestDto);
            var newOrder = await _orderRepository.CreateOrder(orderDomainModel);
            return Ok(_mapper.Map<OrderDto>(newOrder));
        }

        [HttpGet]
        [Route("{orderUserId}")]
        public async Task<IActionResult> GetLastOrder([FromRoute] int orderUserId)
        {
            var order = await _orderRepository.GetLastOrder(orderUserId);
            if(order.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<OrderDto>>(order));
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllOrders(string userId)
        {
            var orders = await _orderRepository.GetAllOrders(userId);
            return Ok(_mapper.Map<List<OrderDto>>(orders));
        }
    }
}
