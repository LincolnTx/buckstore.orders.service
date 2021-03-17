using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.ProductName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("product_name")
                .IsRequired();

            builder.Property(item => item.Quantity)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(item => item.Price)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("price")
                .IsRequired();
        }
    }
}