using FluentValidation;
using OnlineCoffeeShop.Application.Product.Commands.Create;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;

namespace OnlineCoffeeShop.Application.Product.Commands.Common;
public class ProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public ProductCommandValidator()
    {
        this.RuleFor(c => c.Name)
            .MinimumLength(MinNameLength)
            .MaximumLength(MaxNameLength)
            .NotEmpty();


        //this.RuleFor(z => z.Currency)
        //    .MinimumLength(CurrencyLength)
        //    .MaximumLength(CurrencyLength)
        //    .NotEmpty();

        this.RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("'{PropertyName}' must be a valid url.");

        this.RuleFor(f => f.Quantity)
            .GreaterThan(0);

        this.RuleFor(z => z.Price)
            .GreaterThan(0);

    }
}
