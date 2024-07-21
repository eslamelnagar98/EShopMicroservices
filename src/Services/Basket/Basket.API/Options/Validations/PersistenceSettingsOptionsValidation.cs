namespace Basket.API.Options.Validations;
public class PersistenceSettingsOptionsValidation : AbstractValidator<PersistenceSettingsOptions>
{
    public PersistenceSettingsOptionsValidation()
    {
        RuleFor(p => p.ConnectionString)
            .NotEmpty()
            .WithMessage("Connection String Is Required");

        RuleFor(p => p.Redis)
            .NotEmpty()
            .WithMessage("Redis Connection Is Required");

        RuleFor(p => p.Cron)
            .NotEmpty()
            .WithMessage("Cron Connection Is Required");
    }
}
