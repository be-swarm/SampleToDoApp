using BeSwarm.CoreBlazorApp.Services;
using BeSwarm.CoreBlazorApp;
using BeSwarm.WebApi;
using CommonLibPages;
using BeSwarm.CoreBlazorApp.Components;

using BlazorServerToDoApp;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CommonLibPages.ViewModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();


builder.Services.AddCoreBlazorApp();
// inject blazor specific login service
builder.Services.AddScoped<ILoginBeSwarmService, BlazorLoginBeSwarmService>();
builder.Services.AddScoped<ISessionPersistence,SessionPersistenceToLocalWeb> ();
builder.Services.AddScoped<BeSwarmEnvironment>();
builder.Services.AddSingleton<ISecureConfig, SecureConfig>();
builder.Services.AddSingleton<ICryptoService,CryptoFromNativeNetCore>();

builder.Services.AddCommonLibPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseRequestLocalization(new RequestLocalizationOptions()
	.AddSupportedCultures(new[] { "en", "fr" })
	.AddSupportedUICultures(new[] { "en", "fr" }));


app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
