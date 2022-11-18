using BeSwarm.CoreBlazorApp.Components;
using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorToDoApp.Shared
{
	public partial class MainLayout : IDisposable
	{
		[CascadingParameter] BeSwarmEnvironment Session { get; set; } = default!;

		protected override async Task OnAfterRenderAsync(bool FirstTime)
		{
			if (FirstTime)
			{
				Session.EnvironmentHasChanged += async (ChangeEvents e) => await Refresh(e);
			}
		}

		private async Task Refresh(ChangeEvents e)
		{
			StateHasChanged();
		}
		void IDisposable.Dispose()
		{
			Session.EnvironmentHasChanged -= async (ChangeEvents e) => await Refresh(e);
		}
	}
}
