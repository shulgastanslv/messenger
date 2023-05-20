using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(i => i.Password).NotEmpty();
        RuleFor(i => i.UserName).NotEmpty();
        RuleFor(i => i.Email).NotEmpty();
    }
}