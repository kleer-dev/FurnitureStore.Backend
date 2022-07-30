using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FurnitureStore.Application.CommandsQueries.Order.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : BaseController
{
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
}
