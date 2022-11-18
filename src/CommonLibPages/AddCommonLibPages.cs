using CommonLibPages.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSwarm.CoreBlazorApp;
using BeSwarm.CoreBlazorApp.Services;

namespace CommonLibPages
{
	public static class CoreCommonLibPages
	{
		public static IServiceCollection AddCommonLibPages(this IServiceCollection services)
		{
			services.AddScoped<ViewModelToDo>();
			return services;
		}
	}
}
