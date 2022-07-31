using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;

public class CreateFurnitureCommandValidator : AbstractValidator<CreateFurnitureCommand>
{
    public CreateFurnitureCommandValidator()
    {
        RuleFor(f => f.Name).NotEmpty().MaximumLength(255);
        RuleFor(f => f.Price).NotEmpty().GreaterThan(0);
        RuleFor(f => f.Lenght).NotEmpty().GreaterThan(0);
        RuleFor(f => f.Height).NotEmpty().GreaterThan(0);
        RuleFor(f => f.Material).NotEmpty().MaximumLength(255);
        RuleFor(f => f.FurnitureTypeId).NotEmpty();
        RuleFor(f => f.CompanyId).NotEmpty();
    }
}
