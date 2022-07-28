using AutoMapper;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Delete;
using FurnitureStore.WebApi.Dto.FurnitureType;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/furniture-types")]
public class FurnitureTypeController : BaseController
{
    private readonly IMapper _mapper;

    public FurnitureTypeController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateFurnitureTypeDto dto)
    {
        var command = _mapper.Map<CreateFurnitureTypeCommand>(dto);
        var furnitureTypeId = await Mediator.Send(command);

        return Created("api/furniture-types", furnitureTypeId);
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteFurnitureTypeCommand() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}
