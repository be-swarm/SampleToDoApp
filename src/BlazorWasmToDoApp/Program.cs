using BlazorWasmToDoApp;
using BeSwarm.CoreBlazorApp.Services;
using BeSwarm.CoreBlazorApp;
using BeSwarm.WebApi;
using CommonLibPages;
using BeSwarm.CoreBlazorApp.Components;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddCoreBlazorApp();
builder.Services.AddScoped<ILoginBeSwarmService, BlazorLoginBeSwarmService>();
builder.Services.AddScoped<ISessionPersistence, SessionPersistenceToLocalWeb>();
builder.Services.AddScoped<BeSwarmEnvironment>();
builder.Services.AddSingleton<ISecureConfig, SecureConfig>();
builder.Services.AddSingleton<ICryptoService, CryptoFromJS >();

builder.Services.AddCommonLibPages();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
