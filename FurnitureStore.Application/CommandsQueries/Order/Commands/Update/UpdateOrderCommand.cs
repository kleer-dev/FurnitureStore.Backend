using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Update;

public class UpdateOrderCommand : IRequest
{
    public long? UserId { get; set; }
    public long OrderId { get; set; }
    public long FurnitureId { get; set; }
}
