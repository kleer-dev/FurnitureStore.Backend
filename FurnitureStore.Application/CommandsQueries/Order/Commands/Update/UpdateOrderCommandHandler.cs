using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly ICacheManager<Domain.Order> _cacheManager;

    public UpdateOrderCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.Order> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && 
                o.User.Id == request.UserId && o.IsCompleted == false, cancellationToken);

        var furniture = await _dbContext.Furnitures
            .FirstOrDefaultAsync(f => f.Id == request.FurnitureId, cancellationToken);

        if (order == null)
            throw new NotFoundException(nameof(Domain.Order), request.OrderId);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.FurnitureId);

        order.Furniture = furniture;

        await _dbContext.SaveChangesAsync(cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(order.Id, order);

        return Unit.Value;
    }
}
