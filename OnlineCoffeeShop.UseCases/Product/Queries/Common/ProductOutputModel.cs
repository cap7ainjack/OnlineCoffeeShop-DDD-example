namespace OnlineCoffeeShop.Application.Product.Queries.Common;

public record ProductOutputModel
{
    public int Id { get; set; } 
    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }
}
