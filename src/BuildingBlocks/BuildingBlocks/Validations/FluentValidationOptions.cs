namespace BuildingBlocks.Validations;
public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    private readonly IValidator<TOptions> _validator;
    public FluentValidationOptions(IValidator<TOptions> validator)
    {
        _validator = validator;
    }
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var validatorResult = _validator.Validate(options);
        var errors = validatorResult.Errors.Select(x =>
        $"Options Validation Failed For {x.PropertyName} With Error {x.ErrorMessage}");
        return validatorResult.IsValid ? ValidateOptionsResult.Success : ValidateOptionsResult.Fail(errors);
    }


}
