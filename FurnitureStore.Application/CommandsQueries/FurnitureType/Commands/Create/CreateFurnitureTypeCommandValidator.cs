using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;

public class CreateFurnitureTypeCommandValidator : AbstractValidator<CreateFurnitureTypeCommand>
{
    public CreateFurnitureTypeCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}
