using BeSwarm.CoreBlazorApp;
using BeSwarm.CoreBlazorApp.Components;
using BeSwarm.CoreBlazorApp.Services;
using BeSwarm.WebApi;
using CommonLibPages;


using Microsoft.Extensions.Logging;

namespace MauiBlazorToDoApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services.AddMauiBlazorWebView();

			builder.Services.AddCoreBlazorApp();
			// inject blazor specific login service
			builder.Services.AddScoped<ILoginBeSwarmService, MauiAuthenticator>();
			builder.Services.AddScoped<ISessionPersistence, SessionPersistenceMaui>();
			builder.Services.AddSingleton<ISecureConfig, SecureConfig>();
			builder.Services.AddSingleton<ICryptoService, CryptoFromNativeNetCore>();

			builder.Services.AddCommonLibPages();




#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}