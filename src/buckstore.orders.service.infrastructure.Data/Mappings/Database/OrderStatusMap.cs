using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
    public class OrderStatusMap : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("order_status");
            
            builder.HasKey(orderStatus => orderStatus.Id);

            builder.Property<int>(orderStatus => orderStatus.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired()
                .HasColumnName("Id");

            builder.Property<string>(orderStatus => orderStatus.Name)
                .HasMaxLength(200)
                .IsRequired()
                .HasColumnName("Status");

            builder.HasData(OrderStatus.Submitted, OrderStatus.StockConfirmation, OrderStatus.Accept, OrderStatus.Cancelled);
        }
    }
}