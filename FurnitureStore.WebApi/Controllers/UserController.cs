using FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;
using FurnitureStore.Application.CommandsQueries.User.Queries.Get;
using FurnitureStore.Auth.Login;
using FurnitureStore.Auth.Registration;
using FurnitureStore.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult<UserDto>> Registration([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<UserVm>> Get()
    {
        var query = new GetUserQuery() { UserId = UserId};
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [HttpPatch("addmoney/{amount:decimal}")]
    public async Task<ActionResult<decimal>> AddMoney(decimal amount)
    {
        var command = new UserAddMoneyCommand()
        {
            UserId = UserId,
            MoneyAmount = amount
        };

        var moneyAmount = await Mediator.Send(command);
        
        return Ok(moneyAmount);
    }
}
