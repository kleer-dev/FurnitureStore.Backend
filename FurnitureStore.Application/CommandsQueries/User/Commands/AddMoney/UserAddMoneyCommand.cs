using MediatR;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;

public class UserAddMoneyCommand : IRequest<decimal>
{
    public long? UserId { get; set; }
    public decimal MoneyAmount { get; set; }
}