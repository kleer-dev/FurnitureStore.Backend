using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Delete;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    public readonly ICacheManager<Domain.Order> _cacheManager;

    public DeleteOrderCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.Order> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && 
                o.User.Id == request.UserId, cancellationToken);

        if (order == null)
            throw new NotFoundException(nameof(Domain.Order), request.OrderId);

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.RemoveCacheValue(order.Id);

        return Unit.Value;
    }
}
