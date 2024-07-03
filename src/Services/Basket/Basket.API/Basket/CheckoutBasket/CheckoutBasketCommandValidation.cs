namespace Basket.API.Basket.CheckoutBasket;
public class CheckoutBasketCommandValidation : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidation()
    {
        RuleFor(x => x.BasketCheckoutDto)
            .NotNull()
            .WithMessage("BasketCheckoutDto can't be null");

        RuleFor(x => x.BasketCheckoutDto.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}