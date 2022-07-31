using FurnitureStore.Application.CommandsQueries.Role.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Role.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Role.Queries.Get;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Route("api/roles")]
public class RoleController : BaseController
{
    [HttpGet("get/{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var query = new GetRoleQuery() { RoleId = id };
        var role = await Mediator.Send(query);
        
        return Ok(role);
    }
    
    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);

        return Created("api/roles", roleId);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteRoleCommand() { RoleId = id };
        await Mediator.Send(command);

        return NoContent();
    }
}