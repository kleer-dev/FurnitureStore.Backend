using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Order.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;
using FurnitureStore.WebApi.Dto.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : BaseController
{
    private readonly IMapper _mapper;

    public OrderController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<OrderVm>> Get(long id)
    {
        var command = new GetOrderQuery() 
        { 
            Id = id,
            UserId = UserId
        };
        var order = await Mediator.Send(command);

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var query = new GetOrderListQuery() { UserId = UserId };
        var vm = await Mediator.Send(query);

        return Ok(vm.Orders);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateOrderDto dto)
    {
        var command = _mapper.Map<CreateOrderCommand>(dto);
        command.UserId = UserId;

        var orderId = await Mediator.Send(command);

        return Created("api/orders", orderId);
    }
}
