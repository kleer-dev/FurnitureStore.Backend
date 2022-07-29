using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Delete;

public class DeleteFurnitureCommand : IRequest
{
    public long Id { get; set; }
}
