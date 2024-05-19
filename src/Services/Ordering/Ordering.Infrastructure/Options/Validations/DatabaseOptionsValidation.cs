namespace Ordering.Infrastructure.Options.Validations;
public class DatabaseOptionsValidation : AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidation()
    {
        RuleFor(d => d.ConnectionString)
            .NotEmpty()
            .WithMessage("Connection String Is Required");
    }
}
