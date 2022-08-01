using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Update;
using FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Furniture.Queries.GetList;
using FurnitureStore.WebApi.Dto.Furniture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/furniture")]
public class FurnitureController : BaseController
{
    private readonly IMapper _mapper;

    public FurnitureController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [ResponseCache(CacheProfileName = "QueryCache")]
    [HttpGet("get/{id:long}")]
    public async Task<ActionResult<FurnitureVm>> Get(long id)
    {
        var query = new GetFurnitureQuery { Id = id };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [AllowAnonymous]
    [ResponseCache(CacheProfileName = "QueryCache")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<FurnitureDto>>> GetAll()
    {
        var query = new GetFurnitureListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm.Furnitures);
    }

    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateFurnitureCommand furniture)
    {
        var furnitureId = await Mediator.Send(furniture);

        return Created("api/furniture", furnitureId);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteFurnitureCommand { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("update/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateFurnitureDto dto)
    {
        var command = _mapper.Map<UpdateFurnitureCommand>(dto);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }
}
