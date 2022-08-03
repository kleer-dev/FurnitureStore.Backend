using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.AddMoney;

public class UserAddMoneyValidator : AbstractValidator<UserAddMoneyCommand>
{
    public UserAddMoneyValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.MoneyAmount).NotEmpty().GreaterThan(0);
    }
}