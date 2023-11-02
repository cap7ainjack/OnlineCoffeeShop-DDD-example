using MediatR;
using OnlineCoffeeShop.Application.Product.Queries.Common;

namespace OnlineCoffeeShop.Application.Product.Queries.ById;
public class GetProductById : IRequest<ProductOutputModel>
{
    public GetProductById(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
};