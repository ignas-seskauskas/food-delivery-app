﻿using FoodDeliveryApi.Data;
using FoodDeliveryApi.Interfaces.Repositories;
using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly FoodDeliveryDbContext _dbContext;

        public StoreRepository(FoodDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Store>> GetAllStores()
        {
            return await _dbContext.Stores.ToListAsync();
        }

        public async Task<Store?> GetStoreById(long id)
        {
            return await _dbContext.Stores.FindAsync(id);
        }

        public async Task<Store> CreateStore(Store store)
        {
            try
            {
                await _dbContext.Stores.AddAsync(store);
                await _dbContext.SaveChangesAsync();
                return store;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
