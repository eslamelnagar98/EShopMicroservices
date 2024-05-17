namespace Basket.API.Options.Validations;
public class GrpcSettingsOptionsValidation : AbstractValidator<GrpcSettingsOptions>
{
    public GrpcSettingsOptionsValidation()
    {
        RuleFor(grpc => grpc.DiscountUrl)
            .NotEmpty()
            .WithMessage("Discount.gRPC Server URl Is Required");
    }
}
