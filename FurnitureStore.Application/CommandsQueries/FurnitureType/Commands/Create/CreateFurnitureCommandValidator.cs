using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;

public class CreateFurnitureCommandValidator : AbstractValidator<CreateFurnitureTypeCommand>
{
    public CreateFurnitureCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}
