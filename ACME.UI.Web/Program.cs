using ACME.Business.Calculator;
using ACME.Business.Interfaces;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("CON_APPCONFIG");
builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(opts => {
        opts.Connect(connectionString)
            .Select("\\*", "Production")
            .UseFeatureFlags();
    }, false);
});

// Add services to the container.
builder.Services.AddTransient<ICalculator, Calculator>();
builder.Services.AddAzureAppConfiguration().AddFeatureManagement();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAzureAppConfiguration();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}");

app.Run();

public partial class Program { }
