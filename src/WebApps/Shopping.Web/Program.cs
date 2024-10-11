var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddCustomFluentValidation(Assembly.GetAssembly(typeof(Program)) ?? Assembly.GetExecutingAssembly())
       .AddApiSettingsOptions(ApiSettingsOptions.SectionName)
       .AddCustomRefitClients()
       .AddCustomRazorPages();

builder.Host.UseCustomNLog();

var app = builder.Build();
app.ConfigureMiddleware();

app.Run();