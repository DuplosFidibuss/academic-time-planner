using AcademicTimePlanner;
using AcademicTimePlanner.Data;
using AcademicTimePlanner.Services.DataManagerService;
using AcademicTimePlanner.Services.TogglApiService;
using AcademicTimePlanner.Services.TogglService;
using Blazored.LocalStorage;
using Blazored.LocalStorage.Serialization;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.Replace(ServiceDescriptor.Scoped<IJsonSerializer, NewtonsoftJsonSerializer>());
var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options => options
    .ScanAssemblies(currentAssembly)
    .UseReduxDevTools());

builder.Services.AddScoped<ITogglApiService, TogglApiService>();
builder.Services.AddScoped<ITogglService, TogglService>();
builder.Services.AddScoped<IDataManagerService, DataManagerService>();

await builder.Build().RunAsync();
