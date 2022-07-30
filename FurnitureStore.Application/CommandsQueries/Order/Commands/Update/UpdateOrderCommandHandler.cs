using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public UpdateOrderCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && 
                o.User.Id == request.UserId, cancellationToken);

        var furniture = await _dbContext.Furnitures
            .FirstOrDefaultAsync(f => f.Id == request.FurnitureId, cancellationToken);

        if (order == null)
            throw new NotFoundException(nameof(Domain.Order), request.OrderId);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.FurnitureId);

        order.Furniture = furniture;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
