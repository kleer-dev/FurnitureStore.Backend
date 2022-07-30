using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Order.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.UserId).NotEmpty();
        RuleFor(o => o.FurnitureId).NotEmpty();
    }
}
