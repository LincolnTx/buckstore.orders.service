using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");
            
            builder.HasKey(order => order.Id);
            
            builder.Property<int>("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();
            
            builder.Property(order => order.Value)
                .HasField("_value")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("value")
                .IsRequired();
            
            builder.OwnsOne<Address>(order => 
                order.Address, orderAddress =>
            {
                orderAddress.WithOwner();
            });
            
            builder.HasOne(order => order.OrderStatus)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_orderStatusId")
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(order => order.OrderItems)
                .WithOne();

        }
    }
}