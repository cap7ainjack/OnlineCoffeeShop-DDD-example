using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoffeeShop.Domain.Aggregates.Product;

namespace OnlineCoffeeShop.Infrastructure.Configurations;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .OwnsOne(c => c.Price, o =>
            {
                o.WithOwner();

                o.Property(op => op.Price).HasColumnType("decimal(10,2)"); ;
                o.Property(op => op.Currency).HasColumnType("nvarchar(3)"); ;
            });


        builder
            .Property(z => z.ImageUrl)
            .HasColumnType("nvarchar(max)");
    }
}
