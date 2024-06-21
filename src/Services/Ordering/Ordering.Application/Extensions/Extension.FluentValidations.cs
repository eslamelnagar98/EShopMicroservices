namespace Ordering.Application.Extensions;
public static partial class Extension
{
    public static IRuleBuilderOptions<T, TProperty> NotNullMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
       return ruleBuilder.NotEmpty()
                         .WithMessage($"{typeof(TProperty).Name} is required");
    }

    public static IRuleBuilderOptions<T, TProperty> NotEmptyMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty()
                          .WithMessage($"{typeof(TProperty).Name} is required");
    }
}
