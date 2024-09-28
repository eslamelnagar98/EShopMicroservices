var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
builder.Services.AddReverseProxyFromConfig(builder.Configuration)
                .AddSlidingWindowRateLimiting();
var app = builder.Build();
app.UseRateLimiter();
app.MapReverseProxy();
app.Run();