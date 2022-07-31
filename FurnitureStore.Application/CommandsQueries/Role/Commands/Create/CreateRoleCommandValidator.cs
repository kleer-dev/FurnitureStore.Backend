using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(50);
    }
}