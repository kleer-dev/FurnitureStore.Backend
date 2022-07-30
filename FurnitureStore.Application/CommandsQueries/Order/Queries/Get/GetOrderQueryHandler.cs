using AutoMapper;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.Get;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderVm> Handle(GetOrderQuery request, 
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.Company)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.FurnitureType)
            .FirstOrDefaultAsync(o => o.Id == request.Id && 
                o.User.Id == request.UserId, cancellationToken);

        order.Furniture.Orders = null!;

        if (order == null)
            throw new NotFoundException(nameof(Domain.Order), request.Id);

        return _mapper.Map<OrderVm>(order);
    }
}
