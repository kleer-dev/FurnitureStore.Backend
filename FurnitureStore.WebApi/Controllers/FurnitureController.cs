using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/furnitures")]
public class FurnitureController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateFurnitureCommand furniture)
    {
        var furnitureId = await Mediator.Send(furniture);

        return Created("api/furnitures", furnitureId);
    }
}
