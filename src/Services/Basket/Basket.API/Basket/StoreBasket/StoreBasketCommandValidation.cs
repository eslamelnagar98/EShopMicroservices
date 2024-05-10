﻿namespace Basket.API.Basket.StoreBasket;
internal sealed class StoreBasketCommandValidation : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidation()
    {
        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Cart can not be null");

        RuleFor(x => x.Cart.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}
