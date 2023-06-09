﻿using FluentValidation;

namespace Application.Users.Commands.UserRegistration;

public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
{
    public UserRegistrationCommandValidator()
    {
        RuleFor(i => i.Password)
            .NotEmpty().WithMessage("Password cannot be empty.");

        RuleFor(i => i.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.");
    }
}