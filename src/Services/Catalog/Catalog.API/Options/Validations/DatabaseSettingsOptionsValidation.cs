namespace Catalog.API.Options.Validations;
public class DatabaseSettingsOptionsValidation : AbstractValidator<DatabaseSettingsOptions>
{
    public DatabaseSettingsOptionsValidation()
    {
        RuleFor(d => d.ConnectionString)
            .NotEmpty()
            .WithMessage("Connection String Is Required");
    }
}
