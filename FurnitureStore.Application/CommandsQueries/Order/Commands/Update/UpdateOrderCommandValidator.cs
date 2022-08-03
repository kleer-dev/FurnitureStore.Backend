using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.UserId).NotEmpty();
        RuleFor(o => o.OrderId).NotEmpty();
        RuleFor(o => o.FurnitureId).NotEmpty();
    }
}
