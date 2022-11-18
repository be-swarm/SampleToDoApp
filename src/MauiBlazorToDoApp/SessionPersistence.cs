using BeSwarm.CoreBlazorApp.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorToDoApp
{
	public class SessionPersistenceMaui : ISessionPersistence
	{


		public SessionPersistenceMaui()
		{
		}
		public async Task Save(string key, string value)
		{
			await SecureStorage.Default.SetAsync(key, value);
		}

		public async Task<string> Get(string key)
		{
			return await SecureStorage.Default.GetAsync(key);

		}
	}
}
