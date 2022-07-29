using System.Security.Claims;
using FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var query = new GetOrderListQuery() { User = UserId };
        var vm = await Mediator.Send(query);

        return Ok(vm.Orders);
    }
}
