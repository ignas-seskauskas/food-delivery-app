﻿using FluentValidation;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Validators
{
    public class PartnerValidator : AbstractValidator<Partner>
    {
        public PartnerValidator()
        {
            Include(new UserValidator());
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status is not valid");
        }
    }
}
