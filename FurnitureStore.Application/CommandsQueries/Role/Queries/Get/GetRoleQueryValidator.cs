using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Role.Queries.Get;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
        RuleFor(r => r.RoleId).NotEmpty();
    }
}