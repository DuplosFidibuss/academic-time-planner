using AcademicTimePlanner;
using AcademicTimePlanner.Services.BootstrapModalService;
using AcademicTimePlanner.Services.TogglApiService;
using AcademicTimePlanner.Services.TogglService;
using AcademicTimePlanner.Store.Middleware.Persistence;
using AcademicTimePlanner.Store.Middleware.Setup;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options => options
    .ScanAssemblies(currentAssembly)
    .UseReduxDevTools()
    .AddMiddleware<PersistenceMiddleware>()
    .AddMiddleware<SetupMiddleware>());

builder.Services.AddScoped<ITogglApiService, TogglApiService>();
builder.Services.AddScoped<ITogglService, TogglService>();
builder.Services.AddScoped<IBootstrapModalService, BootstrapModalService>();

await builder.Build().RunAsync();
