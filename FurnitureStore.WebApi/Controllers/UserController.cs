using AutoMapper;
using FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;
using FurnitureStore.Application.CommandsQueries.User.Commands.Update;
using FurnitureStore.Application.CommandsQueries.User.Queries.Get;
using FurnitureStore.Auth;
using FurnitureStore.Auth.Commands.Login;
using FurnitureStore.Auth.Commands.RefreshToken;
using FurnitureStore.Auth.Commands.Registration;
using FurnitureStore.WebApi.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

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
    
    [ResponseCache(CacheProfileName = "QueryCache")]
    [HttpGet("account")]
    public async Task<ActionResult<UserVm>> Get()
    {
        var query = new GetUserQuery { UserId = UserId};
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }
    
    [HttpPatch("add-money/{amount:decimal}")]
    public async Task<ActionResult<decimal>> AddMoney(decimal amount)
    {
        var command = new UserAddMoneyCommand
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

    [HttpPut("update-account")]
    public async Task<ActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var command = _mapper.Map<UpdateUserCommand>(dto);
        command.Id = UserId;
        
        await Mediator.Send(command);
        
        return NoContent();
    }
}
