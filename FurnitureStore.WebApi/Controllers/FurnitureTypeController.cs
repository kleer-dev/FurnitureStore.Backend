using AutoMapper;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.GetList;
using FurnitureStore.WebApi.Dto.FurnitureType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/furniture-types")]
public class FurnitureTypeController : BaseController
{
    private readonly IMapper _mapper;

    public FurnitureTypeController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<ActionResult<FurnitureTypeVm>> Get(long id)
    {
        var query = new GetFurnitureTypeQuery { Id = id };
        var furnitureType = await Mediator.Send(query);

        return Ok(furnitureType);
    }

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<FurnitureTypeDto>>> GetAll()
    {
        var query = new GetFurnitureTypeListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm.FurnitureTypes);
    }

    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateFurnitureTypeDto dto)
    {
        var command = _mapper.Map<CreateFurnitureTypeCommand>(dto);
        var furnitureTypeId = await Mediator.Send(command);

        return Created("api/furniture-types", furnitureTypeId);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteFurnitureTypeCommand { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("update/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateFurnitureTypeDto dto)
    {
        var command = _mapper.Map<UpdateFurnitureTypeCommand>(dto);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }
}
