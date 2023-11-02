using MediatR;
using OnlineCoffeeShop.Domain.Factories.Product;

namespace OnlineCoffeeShop.Application.Product.Commands.Create;
public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductFactory productFactory;
    private readonly IProductRepository _repository;

    public CreateProductHandler(
        IProductRepository repository,
        IProductFactory productFactory)
    {
        _repository = repository;
        this.productFactory = productFactory;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = productFactory
            .WithName(request.Name)
            .WithDescription(request.Description)
            .WithQuantity(request.Quantity)
            .WithPrice(request.Price, request.Currency)
            .WithImage(request.ImageUrl)
            .Build();

        var added = await _repository.AddAsync(product);

        await _repository.SaveAsync(product);

        return added.Id;
    }
};