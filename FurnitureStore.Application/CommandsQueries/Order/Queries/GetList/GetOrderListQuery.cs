using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;

public class GetOrderListQuery : IRequest<GetOrderListVm>
{
    public long? UserId { get; set; }
}
