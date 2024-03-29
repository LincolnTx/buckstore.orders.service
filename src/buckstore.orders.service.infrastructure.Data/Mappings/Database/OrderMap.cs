﻿using System;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
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

            builder.Ignore(o => o.DomainEvents);

            builder.Property<Guid>("_buyerid")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BuyerId")
                .IsRequired();

            builder.Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();
            
            builder.Property<int>("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            builder.Property<Guid>("_paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId");
            
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

            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("_paymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_buyerid");

        }
    }
}