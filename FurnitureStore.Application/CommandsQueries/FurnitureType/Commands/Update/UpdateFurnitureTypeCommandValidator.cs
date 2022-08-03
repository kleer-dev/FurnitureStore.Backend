using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;

public class UpdateFurnitureTypeCommandValidator : AbstractValidator<UpdateFurnitureTypeCommand>
{
    public UpdateFurnitureTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
    }
}
