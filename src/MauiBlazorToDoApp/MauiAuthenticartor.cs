using BeSwarm.CoreBlazorApp.Services;
using IdentityModel.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorToDoApp
{
	public class MauiAuthenticator : ILoginBeSwarmService
	{
		public async Task<LoginAction> Login(string url, string callbackurl)
		{
			LoginAction result = new();

			try
			{
				WebAuthenticatorResult res = await WebAuthenticator.Default.AuthenticateAsync(
					new Uri(url),
					new Uri(callbackurl));

				result.CallBackUrl = new RequestUrl(callbackurl).Create(new Parameters(res.Properties));
				result.Action = LoginActions.gettokens;
			}
			catch (TaskCanceledException)
			{
			}
			return result;
		}

	}
}
