using MediatR;
using OnlineCoffeeShop.Application.Product.Queries.Common;
using OnlineCoffeeShop.Domain.Interfaces;

namespace OnlineCoffeeShop.Application.Product.Queries.ById;
public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductOutputModel>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductOutputModel> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var foundedProduct = await _repository.GetDetails(request.Id, cancellationToken);

        if (foundedProduct == null)
        {
            throw new KeyNotFoundException(request.Id.ToString());
        }

        return foundedProduct;
    }
}