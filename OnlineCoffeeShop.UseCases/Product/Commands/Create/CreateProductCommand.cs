using MediatR;

namespace OnlineCoffeeShop.Application.Product.Commands.Create;
public class CreateProductCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public decimal Price { get; set; }
    public string Currency { get; set; }

    public int Quantity { get; set; }

    public string ImageUrl { get; set; }
};
