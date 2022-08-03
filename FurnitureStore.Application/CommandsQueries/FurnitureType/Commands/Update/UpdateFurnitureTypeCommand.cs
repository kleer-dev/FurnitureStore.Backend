using MediatR;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;

public class UpdateFurnitureTypeCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}
