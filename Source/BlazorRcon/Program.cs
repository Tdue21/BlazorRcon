using BlazorRcon.Common.Interfaces;
using BlazorRcon.Common.Services;
using BlazorRcon.Interfaces;
using BlazorRcon.Services;
using BlazorRcon.ViewModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            //.WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(le => le.))
            .CreateLogger();
try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, config) =>
                            {
                                config.ReadFrom.Configuration(context.Configuration);
                            });

    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    builder.Services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddTransient<ProtectedBrowserStorage, ProtectedLocalStorage>();
    builder.Services.AddTransient<ILocalizationService, LocalizationService>();
    builder.Services.AddTransient<IBrowserStorage, BrowserStorage>();
    builder.Services.AddTransient<IRconClient, RconClient>();
    builder.Services.AddTransient<MainViewModel>();

    builder.Services.AddMudServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}