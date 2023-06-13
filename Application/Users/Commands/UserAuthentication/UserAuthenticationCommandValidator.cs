using FluentValidation;

namespace Application.Users.Commands.UserAuthentication;

public class UserAuthenticationCommandValidator : AbstractValidator<UserAuthenticationCommand>
{
    public UserAuthenticationCommandValidator()
    {
        RuleFor(i => i.Password)
            .NotEmpty().WithMessage("Password cannot be empty.");

        RuleFor(i => i.Username)
            .NotEmpty().WithMessage("Username cannot be empty.");
    }
}