using MediatR;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Delete;

public class DeleteFurnitureTypeCommand : IRequest
{
    public long Id { get; set; }
}
