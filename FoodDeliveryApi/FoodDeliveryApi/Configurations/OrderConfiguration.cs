﻿using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDeliveryApi.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IsCanceled).HasDefaultValue(false);

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.ItemsPrice).IsRequired();

            builder.Property(x => x.DeliveryFee).IsRequired();

            builder.Property(x => x.TotalPrice).IsRequired();

            builder.HasOne(x => x.Store).WithMany(x => x.Orders).HasForeignKey(x => x.StoreId);

            builder.Ignore(x => x.OrderStatus);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);

            builder.Property(x => x.City).IsRequired().HasMaxLength(50);

            builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(10);
        }
    }

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });

            builder.HasOne(x => x.Product).WithMany(x => x.OrderItems).HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.TotalPrice).IsRequired();

            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.ProductPrice).IsRequired();
        }
    }
}
