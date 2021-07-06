﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using buckstore.orders.service.infrastructure.Data.Context;

namespace buckstore.orders.service.infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210706175231_FixOrderEntity")]
    partial class FixOrderEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.BuyerAggregate.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("Cpf")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("buyers");
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<string>("_cardHolderName")
                        .IsRequired()
                        .HasColumnName("CardHolderName")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("_cardNumber")
                        .IsRequired()
                        .HasColumnName("CardNumber")
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.Property<DateTime>("_expiration")
                        .HasColumnName("Expiration")
                        .HasColumnType("timestamp without time zone")
                        .HasMaxLength(25);

                    b.Property<string>("_securityNumber")
                        .IsRequired()
                        .HasColumnName("SecurityNumber")
                        .HasColumnType("character varying(5)")
                        .HasMaxLength(5);

                    b.Property<string>("alias")
                        .IsRequired()
                        .HasColumnName("Alias")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("payment_methods");
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnName("PaymentMethodId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnName("value")
                        .HasColumnType("numeric");

                    b.Property<Guid>("_buyerid")
                        .HasColumnName("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("_orderDate")
                        .HasColumnName("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("_orderStatusId")
                        .HasColumnName("OrderStatusId")
                        .HasColumnType("integer");

                    b.Property<Guid>("_paymentMethodId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_buyerid");

                    b.HasIndex("_orderStatusId");

                    b.HasIndex("_paymentMethodId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnName("product_name")
                        .HasColumnType("text");

                    b.Property<decimal>("_price")
                        .HasColumnName("price")
                        .HasColumnType("numeric");

                    b.Property<int>("_quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("order_item");
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("order_status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "StockConfirmation"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accept"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("buckstore.orders.service.domain.Aggregates.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.HasOne("buckstore.orders.service.domain.Aggregates.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("_buyerid");

                    b.HasOne("buckstore.orders.service.domain.Aggregates.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("_orderStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("buckstore.orders.service.domain.Aggregates.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("_paymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("buckstore.orders.service.domain.Aggregates.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("text");

                            b1.HasKey("OrderId");

                            b1.ToTable("order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("buckstore.orders.service.domain.Aggregates.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("buckstore.orders.service.domain.Aggregates.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
