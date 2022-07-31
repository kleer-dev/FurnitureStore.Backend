using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Update;

public class UpdateFurnitureCommandValidator : AbstractValidator<UpdateFurnitureCommand>
{
    public UpdateFurnitureCommandValidator()
    {
        RuleFor(f => f.Id).NotEmpty();

        RuleFor(f => f.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(f => f.Price).NotEmpty().GreaterThan(0);
        RuleFor(f => f.Lenght).NotEmpty().GreaterThan(0);
        RuleFor(f => f.Height).NotEmpty().GreaterThan(0);

        RuleFor(f => f.Material)
           .NotEmpty()
           .MaximumLength(255);

        RuleFor(f => f.furnitureTypeId).NotEmpty();
        RuleFor(f => f.companyId).NotEmpty();
    }
}
