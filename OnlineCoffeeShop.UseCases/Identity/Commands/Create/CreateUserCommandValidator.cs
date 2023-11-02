using FluentValidation;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;


namespace OnlineCoffeeShop.Application.Identity.Commands.Create;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        this.RuleFor(u => u.Email)
            .MinimumLength(MinEmailLength)
            .MaximumLength(MaxEmailLength)
            .EmailAddress()
            .NotEmpty();

        this.RuleFor(u => u.Password)
            .MaximumLength(MaxNameLength)
            .NotEmpty();

        this.RuleFor(u => u.Name)
            .MinimumLength(MinNameLength)
            .MaximumLength(MaxNameLength)
            .NotEmpty();
    }
}