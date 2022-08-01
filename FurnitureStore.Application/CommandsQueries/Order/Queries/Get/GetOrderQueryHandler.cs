using AutoMapper;
using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.Get;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Order> _cacheManager;

    public GetOrderQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper, 
        ICacheManager<Domain.Order> cacheManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<OrderVm> Handle(GetOrderQuery request, 
        CancellationToken cancellationToken)
    {
        var orderQuery = async () => await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.Company)
            .Include(o => o.Furniture)
                .ThenInclude(o => o.FurnitureType)
            .FirstOrDefaultAsync(o => o.Id == request.Id && 
                o.User.Id == request.UserId, cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var order = await _cacheManager.GetOrSetCacheValue(request.Id, orderQuery);

        order.Furniture.Orders = null!;

        return _mapper.Map<OrderVm>(order);
    }
}
