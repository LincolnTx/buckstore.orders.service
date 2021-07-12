using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("payment_methods");

            builder.HasKey(p => p.Id);
            
            builder.Property<Guid>("BuyerId")
                .IsRequired();
            
            builder.Property<string>("_cardHolderName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardHolderName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("_alias")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Alias")
                .HasMaxLength(200)
                .IsRequired();
            
            builder .Property<string>("_cardNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardNumber")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property<DateTime>("_expiration")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Expiration")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property<string>("_securityNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SecurityNumber")
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}