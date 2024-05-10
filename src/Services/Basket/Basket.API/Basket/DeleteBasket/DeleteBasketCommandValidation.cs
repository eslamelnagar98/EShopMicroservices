namespace Basket.API.Basket.DeleteBasket;
internal sealed class DeleteBasketCommandValidation : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidation()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}