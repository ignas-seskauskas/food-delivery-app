﻿using FoodDeliveryApi.Data;
using FoodDeliveryApi.Enums;
using FoodDeliveryApi.Interfaces.Repositories;
using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FoodDeliveryDbContext _dbContext;

        public AuthRepository(FoodDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserById(long id, UserType userType)
        {
            switch (userType)
            {
                case UserType.Customer:
                    return null;
                case UserType.Partner:
                    return null;
                case UserType.Admin:
                    return await _dbContext.Admins.FindAsync(id);
                default:
                    throw new ArgumentException("Invalid user type");
            }
        }

        public async Task<User?> GetUserByUsername(string username, UserType userType)
        {
            switch(userType)
            {
                case UserType.Customer:
                    return null;
                case UserType.Partner:
                    return null;
                case UserType.Admin:
                    return await _dbContext.Admins.Where(x => x.Username == username).FirstOrDefaultAsync();
                default:
                    throw new ArgumentException("Invalid user type");
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}