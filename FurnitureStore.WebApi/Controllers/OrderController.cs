using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Buy;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Update;
using FurnitureStore.Application.CommandsQueries.Order.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;
using FurnitureStore.WebApi.Dto.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize]
[Route("api/orders")]
public class OrderController : BaseController
{
    private readonly IMapper _mapper;

    public OrderController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [ResponseCache(CacheProfileName = "QueryCache")]
    [HttpGet("get/{id:long}")]
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

    [ResponseCache(CacheProfileName = "QueryCache")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var query = new GetOrderListQuery { UserId = UserId };
        var vm = await Mediator.Send(query);

        return Ok(vm.Orders);
    }

    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateOrderDto dto)
    {
        var command = _mapper.Map<CreateOrderCommand>(dto);
        command.UserId = UserId;

        var orderId = await Mediator.Send(command);

        return Created("api/orders", orderId);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteOrderCommand
        {
            OrderId = id,
            UserId = UserId,
        };

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("update/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateOrderDto dto)
    {
        var command = _mapper.Map<UpdateOrderCommand>(dto);
        command.OrderId = id;
        command.UserId = UserId;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("buy/{id:long}")]
    public async Task<ActionResult> Buy(long id)
    {
        var command = new BuyOrderCommand
        {
            OrderId = id,
            UserId = UserId
        };

        var orderId = await Mediator.Send(command);

        return Ok(orderId);
    }
}
