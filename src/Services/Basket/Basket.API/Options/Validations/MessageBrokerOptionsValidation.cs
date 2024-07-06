namespace Basket.API.Options.Validations;
public class MessageBrokerOptionsValidation : AbstractValidator<MessageBrokerOptions>
{
    public MessageBrokerOptionsValidation()
    {
        RuleFor(m => m.Host)
            .NotEmpty()
            .WithMessage("Host Is Required Field");

        RuleFor(m => m.UserName)
            .NotEmpty()
            .WithMessage("UserName Is Required Field");

        RuleFor(m => m.Password)
            .NotEmpty()
            .WithMessage("Password Is Required Field");
    }
}
