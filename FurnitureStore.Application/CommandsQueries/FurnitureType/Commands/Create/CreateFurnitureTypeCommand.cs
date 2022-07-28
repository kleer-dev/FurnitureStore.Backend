using MediatR;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;

public class CreateFurnitureTypeCommand : IRequest<long>
{
    public string Name { get; set; }
}
