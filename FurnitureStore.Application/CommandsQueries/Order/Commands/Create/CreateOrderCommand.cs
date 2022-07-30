using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Create;

public class CreateOrderCommand : IRequest<long>
{
    public long? UserId { get; set; }
    public long FurnitureId { get; set; }
}
