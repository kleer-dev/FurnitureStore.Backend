using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureStore.Application.Interfaces;
using FurnitureStore.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, GetOrderListVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetOrderListVm> Handle(GetOrderListQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.Company)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.FurnitureType)
            .Where(o => o.User.Id == request.UserId)    
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetOrderListVm() { Orders = orders };
    }
}
