using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Buy;

public class BuyOrderCommandHandler : IRequestHandler<BuyOrderCommand, long>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public BuyOrderCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(BuyOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        var order = await _dbContext.Orders
            .Include(o => o.Furniture)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && 
                o.User.Id == request.UserId && o.IsCompleted == false, cancellationToken);

        if (order == null)
            throw new NotFoundException(nameof(Domain.Order), request.OrderId);

        if (user.Balance < order.Furniture.Price)
            throw new Exception("Not enough money on balance");

        user.Balance -= order.Furniture.Price;
        order.IsCompleted = true;
        order.CompletionDate = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
