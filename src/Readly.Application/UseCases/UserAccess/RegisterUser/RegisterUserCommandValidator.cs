using FluentValidation;

namespace Readly.Application.UseCases.UserAccess.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.Email)
            .MaximumLength(100)
            .NotEmpty().WithMessage("Email is required and cannot be empty")
            .NotNull().WithMessage("Email is required and cannot not be null")
            .EmailAddress().WithMessage("Invalid Email Address");

        RuleFor(v => v.Password)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull();
    }
}
