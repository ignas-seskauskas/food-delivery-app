﻿using FluentValidation;
using FoodDeliveryApi.Dto.Auth;
using FoodDeliveryApi.Dto.Error;
using FoodDeliveryApi.Dto.Store;
using FoodDeliveryApi.Enums;
using FoodDeliveryApi.Exceptions;
using FoodDeliveryApi.Interfaces.Services;
using FoodDeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDeliveryApi.Controllers
{
    [ApiController]
    [Route("/api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores([FromQuery] long? partnerId = null, string? city = null)
        {
            List<GetStoreResponseDto> responseDto = await _storeService.GetStores(partnerId ?? null, city ?? null);

            return Ok(responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(long id)
        {
            GetStoreResponseDto responseDto;

            try
            {
                responseDto = await _storeService.GetStore(id);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(new ErrorResponseDto() { Message = ex.Message });
            }

            return Ok(responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "Partner", Policy = "VerifiedPartner")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequestDto requestDto)
        {
            Claim? idClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId");
            long userId = long.Parse(idClaim!.Value);

            CreateStoreResponseDto responseDto;

            try
            {
                responseDto = await _storeService.CreateStore(userId, requestDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorResponseDto()
                {
                    Message = "One or more validation errors occurred. See the 'Errors' for details.",
                    Errors = ex.Errors.Select(err => err.ErrorMessage).ToList()
                });
            }
            catch (InvalidImageException ex)
            {
                return BadRequest(new ErrorResponseDto() { Message = ex.Message });
            }

            return Ok(responseDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Partner", Policy = "VerifiedPartner")]
        public async Task<IActionResult> UpdateStore(long id, [FromBody] UpdateStoreRequestDto requestDto)
        {
            Claim? idClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId");
            long userId = long.Parse(idClaim!.Value);

            UpdateStoreResponseDto responseDto;

            try
            {
                responseDto = await _storeService.UpdateStore(id, userId, requestDto);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(new ErrorResponseDto() { Message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorResponseDto()
                {
                    Message = "One or more validation errors occurred. See the 'Errors' for details.",
                    Errors = ex.Errors.Select(err => err.ErrorMessage).ToList()
                });
            }
            catch (ActionNotAllowedException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponseDto()
                {
                    Message = ex.Message
                });
            }

            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStore(long id)
        {
            DeleteStoreResponseDto responseDto;

            try
            {
                responseDto = await _storeService.DeleteStore(id);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(new ErrorResponseDto() { Message = ex.Message });
            }

            return Ok(responseDto);
        }

        [HttpPut("{id}/image")]
        [Authorize(Roles = "Partner", Policy = "VerifiedPartner")]
        public async Task<IActionResult> UploadImage(long id, [FromForm] IFormFile image)
        {
            Claim? idClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId");
            long userId = long.Parse(idClaim!.Value);

            Claim? roleClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            UserType userType = (UserType)Enum.Parse(typeof(UserType), roleClaim!.Value);

            ImageResponseDto responseDto;

            try
            {
                responseDto = await _storeService.UploadImage(id, userId, image);
            }
            catch (InvalidImageException ex)
            {
                return BadRequest(new ErrorResponseDto() { Message = ex.Message });
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(new ErrorResponseDto() { Message = ex.Message });
            }
            catch (ActionNotAllowedException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponseDto()
                {
                    Message = ex.Message
                });
            }

            return Ok(responseDto);
        }
    }
}
