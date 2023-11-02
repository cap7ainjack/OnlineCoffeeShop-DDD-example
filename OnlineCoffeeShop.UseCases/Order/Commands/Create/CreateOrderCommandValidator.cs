using FluentValidation;

namespace OnlineCoffeeShop.Application.Order.Commands.Create;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(IOrderRepository carAdRepository)
        => this.Include(new CreateOrderCommandValidator(carAdRepository));
}

