using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public CreateOrderCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var furniture = await _dbContext.Furnitures
            .FirstOrDefaultAsync(f => f.Id == request.FurnitureId, cancellationToken);

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.FurnitureId);

        var order = new Domain.Order()
        {
            User = user,
            Furniture = furniture
        };

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
