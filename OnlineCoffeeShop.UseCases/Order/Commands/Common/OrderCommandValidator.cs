using FluentValidation;
using OnlineCoffeeShop.Application.Order.Commands.Create;

namespace OnlineCoffeeShop.Application.Order.Commands.Common;
public class OrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public OrderCommandValidator()
    {
        this.RuleFor(c => c.OrderLines)
            .NotNull()
            .NotEmpty()
            .ForEach(z =>
                z.Must(f => f.Quantity > 0)
                 .Must(g => g.ProductId > 0));
    }
}
