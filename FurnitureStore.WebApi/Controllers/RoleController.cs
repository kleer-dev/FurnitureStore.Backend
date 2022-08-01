using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Role.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Role.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Role.Commands.SetRole;
using FurnitureStore.Application.CommandsQueries.Role.Commands.Update;
using FurnitureStore.Application.CommandsQueries.Role.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Role.Queries.GetList;
using FurnitureStore.WebApi.Dto.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/roles")]
public class RoleController : BaseController
{
    private readonly IMapper _mapper;

    public RoleController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("get/{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var query = new GetRoleQuery { RoleId = id };
        var role = await Mediator.Send(query);
        
        return Ok(role);
    }
    
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
    {
        var query = new GetRoleListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm.Roles);
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
        var command = new DeleteRoleCommand { RoleId = id };
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("update/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateRoleDto dto)
    {
        var command = _mapper.Map<UpdateRoleCommand>(dto);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("set-role")]
    public async Task<ActionResult> SetRole([FromBody] SetRoleDto dto)
    {
        var command = _mapper.Map<SetRoleCommand>(dto);
        await Mediator.Send(command);

        return NoContent();
    }
}