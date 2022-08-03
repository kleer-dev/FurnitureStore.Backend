using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Delete;

public class DeleteOrderCommand : IRequest
{
    public long OrderId { get; set; }
    public long? UserId { get; set; }
}
