﻿using FoodDeliveryApi.Dto.Auth;
using FoodDeliveryApi.Dto.Product;

namespace FoodDeliveryApi.Interfaces.Services
{
    public interface IProductService
    {
        public Task<List<GetProductResponseDto>> GetProducts(long? storeId);
        public Task<GetProductResponseDto> GetProduct(long id);
        public Task<CreateProductResponseDto> CreateProduct(long partnerId, CreateProductRequestDto requestDto);
        public Task<UpdateProductResponseDto> UpdateProduct(long id, long partnerId, UpdateProductRequestDto requestDto);
        public Task<DeleteProductResponseDto> DeleteProduct(long id, long partnerId);
        public Task<ImageResponseDto> UploadImage(long productId, long partnerId, IFormFile image);
    }
}
