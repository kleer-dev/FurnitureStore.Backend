using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Buy;

public class BuyOrderCommand : IRequest<long>
{
    public long? UserId { get; set; }
    public long OrderId { get; set; }
}
