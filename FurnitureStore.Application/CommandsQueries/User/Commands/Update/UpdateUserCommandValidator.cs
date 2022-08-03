using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
        
        RuleFor(u => u.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .MaximumLength(13)
            .MinimumLength(13);

        RuleFor(u => u.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(u => u.OldPassword)
            .MinimumLength(8)
            .NotEmpty();
        
        RuleFor(u => u.NewPassword)
            .MinimumLength(8)
            .NotEmpty();
    }
}