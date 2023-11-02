using FluentValidation;

namespace OnlineCoffeeShop.Application.Product.Commands.Create;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IProductRepository carAdRepository)
        => this.Include(new CreateProductCommandValidator(carAdRepository));
}

