﻿using FoodDeliveryApi.Dto.Customer;

namespace FoodDeliveryApi.Interfaces.Services
{
    public interface ICustomerService
    {
        public Task<List<GetCustomerResponseDto>> GetCustomers();
        public Task<GetCustomerResponseDto> GetCustomer(long id);
        public Task<RegisterCustomerResponseDto> RegisterCustomer(RegisterCustomerRequestDto requestDto);
        public Task<UpdateCustomerResponseDto> UpdateCustomer(long id, UpdateCustomerRequestDto requestDto);
        public Task<DeleteCustomerResponseDto> DeleteCustomer(long id);
    }
}
