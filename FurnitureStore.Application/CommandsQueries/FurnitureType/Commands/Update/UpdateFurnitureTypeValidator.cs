using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;

public class UpdateFurnitureTypeValidator : AbstractValidator<UpdateFurnitureTypeCommand>
{
    public UpdateFurnitureTypeValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(150);
    }
}
