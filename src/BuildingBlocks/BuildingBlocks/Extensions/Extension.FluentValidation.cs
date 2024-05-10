namespace BuildingBlocks.Extensions;
public partial class Extension
{
    public static IServiceCollection AddIOptions<TOptions>(this IServiceCollection services, string sectionName)
       where TOptions : class
    {
        services
            .AddOptionsWithValidateOnStart<TOptions>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(sectionName)
            .Bind(options))
            .ValidateFluently();
        return services;
    }

    private static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(servciceProvider =>
        {
            using var scope = servciceProvider.CreateScope();
            return new FluentValidationOptions<TOptions>(scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>());
        });
        return optionsBuilder;
    }
}

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
