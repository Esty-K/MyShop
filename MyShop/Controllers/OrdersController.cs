using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService service;
        IMapper mapper;
        public OrdersController(IOrderService service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await service.GetById(id);
            if (order != null) {
                OrderDTO orderDTO = mapper.Map<Order, OrderDTO>(order);
                return Ok(orderDTO);
            }
            return NoContent();
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostOrderDTO orderDTO)
        {

            Order order = mapper.Map<PostOrderDTO, Order>(orderDTO);
            var newOrder = await service.Post(order);
            if (newOrder != null)
            {
                OrderDTO orderdto = mapper.Map<Order, OrderDTO>(newOrder);
                return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, orderdto);
            }
            return BadRequest();

        }





    }
}
