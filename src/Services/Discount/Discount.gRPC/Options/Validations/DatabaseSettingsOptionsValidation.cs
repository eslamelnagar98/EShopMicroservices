namespace Discount.gRPC.Options.Validations;
internal sealed class DatabaseSettingsOptionsValidation : AbstractValidator<DatabaseSettingsOptions>
{
    public DatabaseSettingsOptionsValidation()
    {
        RuleFor(d => d.ConnectionString)
            .NotEmpty()
            .WithMessage("Connection String Is Required");
    }
}
