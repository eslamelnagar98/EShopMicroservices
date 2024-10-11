namespace Shopping.Web.Options.Validations;
public class ApiSettingsOptionsValidation : AbstractValidator<ApiSettingsOptions>
{
    public ApiSettingsOptionsValidation()
    {
        RuleFor(api => api.GatewayAddress)
            .NotEmpty()
            .WithMessage("GatewayAddress is required")
            .Matches(@"^https?:\/\/[a-zA-Z0-9.-]+(:\d+)?$")
            .WithMessage("GatewayAddress must be a valid URL, e.g., http://localhost:6064 or https://localhost:6064");
    }
}
