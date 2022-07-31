using FurnitureStore.Application.CommandsQueries.Role.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Route("api/roles")]
public class RoleController : BaseController
{
    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);

        return Created("api/roles", roleId);
    }
}