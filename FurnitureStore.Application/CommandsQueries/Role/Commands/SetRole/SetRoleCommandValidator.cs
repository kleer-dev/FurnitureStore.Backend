using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.SetRole;

public class SetRoleCommandValidator : AbstractValidator<SetRoleCommand>
{
    public SetRoleCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty();
        RuleFor(r => r.RoleId).NotEmpty();
    }
}