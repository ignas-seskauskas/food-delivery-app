﻿using FoodDeliveryApi.Enums;

namespace FoodDeliveryApi.Dto.Order
{
    public class BaseOrderDto<T>
    {
        public long StoreId { get; set; }
        public List<T> Items { get; set; } = new List<T>();
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
