using System;
using System.Threading.Tasks;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/furnitures")]
public class FurnitureController : BaseController
{
    [HttpGet("{id:long}")]
    public async Task<ActionResult<FurnitureVm>> Get(long id)
    {
        var query = new GetFurnitureQuery() { Id = id };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateFurnitureCommand furniture)
    {
        var furnitureId = await Mediator.Send(furniture);

        return Created("api/furnitures", furnitureId);
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteFurnitureCommand() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}
