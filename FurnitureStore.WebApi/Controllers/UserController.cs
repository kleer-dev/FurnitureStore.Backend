using FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;
using FurnitureStore.Application.CommandsQueries.User.Queries.Get;
using FurnitureStore.Auth.Login;
using FurnitureStore.Auth.Registration;
using FurnitureStore.Auth;
using FurnitureStore.Auth.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Route("api/user")]
public class UserController : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult<AuthResponse>> Registration([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize]
    [HttpGet("get-info")]
    public async Task<ActionResult<UserVm>> Get()
    {
        var query = new GetUserQuery() { UserId = UserId};
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [Authorize]
    [HttpPatch("add-money/{amount:decimal}")]
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

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await Mediator.Send(command);
        
        return Ok(response);
    }
}
