using FluentValidation;

namespace FurnitureStore.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(t => t.Token).NotEmpty();
        RuleFor(t => t.RefreshToken).NotEmpty();
    }
}