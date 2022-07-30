using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.Get;

public class GetOrderQuery : IRequest<OrderVm>
{
    public long Id { get; set; }
    public long? UserId { get; set; }
}
