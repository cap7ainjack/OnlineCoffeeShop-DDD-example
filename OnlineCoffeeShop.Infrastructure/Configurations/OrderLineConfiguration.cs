using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Domain.Aggregates.Order;

namespace OnlineCoffeeShop.Infrastructure.Configurations;
internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder
            .HasKey(c => c.Id);
    }
}
