namespace BuildingBlocks.Extensions;
public partial class Extension
{
    public static IServiceCollection AddIOptions<TOptions>(this IServiceCollection services, string sectionName)
       where TOptions : class
    {
        services
            .AddOptionsWithValidateOnStart<TOptions>()
            .BindConfiguration(sectionName)
            .ValidateFluently();
        return services;
    }

    private static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(serviceProvider =>
        {
            using var scope = serviceProvider.CreateScope();
            return new FluentValidationOptions<TOptions>(scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>());
        });
        return optionsBuilder;
    }
}

public class FluentValidationOptions<TOptions>(IValidator<TOptions> validator) 
    : IValidateOptions<TOptions> where TOptions : class
{
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var validatorResult = validator.Validate(options);
        var errors = validatorResult.Errors.Select(x =>
        $"Options Validation Failed For {x.PropertyName} With Error {x.ErrorMessage}");
        return validatorResult.IsValid ? ValidateOptionsResult.Success : ValidateOptionsResult.Fail(errors);
    }
}
