﻿using FoodDeliveryApi.Dto.Auth;
using FoodDeliveryApi.Enums;

namespace FoodDeliveryApi.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto requestDto);
        public Task ChangePassword(long id, UserType userType, ChangePasswordRequestDto requestDto);
    }
}
