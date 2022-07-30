using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;

public class UserAddMoneyCommandHandler : IRequestHandler<UserAddMoneyCommand, decimal>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public UserAddMoneyCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<decimal> Handle(UserAddMoneyCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        user.Balance += request.MoneyAmount;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Balance;
    }
}