using FurnitureStore.Auth.Login;
using FurnitureStore.Auth.Registration;
using FurnitureStore.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("registration")]
    public async Task<ActionResult<UserDto>> Registration([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
}
