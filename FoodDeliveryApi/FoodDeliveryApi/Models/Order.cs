﻿using FoodDeliveryApi.Enums;
using System.Diagnostics.Eventing.Reader;

namespace FoodDeliveryApi.Models
{
    public class Order
    {
        public long Id { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal ItemsPrice { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalPrice { get; set; }
        public long StoreId { get; set; }
        public Store Store { get; set; } = default!;
        public OrderStatus OrderStatus
        {
            get
            {
                if (IsCanceled)
                {
                    return OrderStatus.Canceled;
                }

                DateTime deliveryDateTime = CreatedAt.AddMinutes(Store.DeliveryTimeInMinutes);
                return DateTime.UtcNow >= deliveryDateTime ? OrderStatus.Completed : OrderStatus.Pending;
            }
        }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
