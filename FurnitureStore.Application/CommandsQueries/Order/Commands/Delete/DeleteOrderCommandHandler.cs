using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Delete;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public DeleteOrderCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
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

        return Unit.Value;
    }
}
