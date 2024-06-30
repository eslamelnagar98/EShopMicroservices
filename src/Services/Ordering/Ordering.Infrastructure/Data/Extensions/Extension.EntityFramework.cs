namespace Ordering.Infrastructure.Data.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddOrderingDbContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<TContext>((serviceProvider, Options) =>
        {
            var connectionString = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>()?.Value.ConnectionString;

            Options.AddInterceptors(serviceProvider.GetRequiredService<ISaveChangesInterceptor>());

            Options.UseSqlServer(connectionString);
        });


        return services;
    }
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedAsync(context);
    }
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
           r.TargetEntry != null &&
           r.TargetEntry.Metadata.IsOwned() &&
           (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

    public static IQueryable<TEntity> EvaluateSpecification<TEntity, Tobj>(this IQueryable<TEntity> inputQuery,
                                                                           Tobj specification,
                                                                           Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate)
    {
        if (specification is bool spec)
        {
            return spec is false ? inputQuery : predicate?.Invoke(inputQuery);
        }
        return specification is null ? inputQuery : predicate?.Invoke(inputQuery);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);

        await SeedProductAsync(context);

        await SeedOrdersWithItemsAsync(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(GetCustomers);
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(GetProducts);
        }
    }

    private static async Task SeedOrdersWithItemsAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(GetOrders());
        }
    }

}
