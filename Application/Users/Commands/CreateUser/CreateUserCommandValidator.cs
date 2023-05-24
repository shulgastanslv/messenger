using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(i => i.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 16 characters long.");

        RuleFor(i => i.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.")
            .Length(3, 20).WithMessage("UserName must be between 3 and 20 characters long.")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("UserName must contain only letters, digits, or underscores.");

        RuleFor(i => i.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be a valid email address.");
    }
}