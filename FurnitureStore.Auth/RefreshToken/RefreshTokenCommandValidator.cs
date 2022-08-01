using FluentValidation;

namespace FurnitureStore.Auth.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(t => t.Token).NotEmpty();
        RuleFor(t => t.RefreshToken).NotEmpty();
    }
}