using CommonLibPages.ViewModels;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibPages.Components;

public partial class CounterToDo : IDisposable
{
	[Inject] ViewModelToDo ViewModel { get; set; } = default!;
	protected override async Task OnAfterRenderAsync(bool FirstTime)
	{
		if (FirstTime)
		{

			ViewModel.PropertyChanged += async (o, e) => OnPropertyChanged(o,e);
		}
		await base.OnAfterRenderAsync(FirstTime);
	}

	private async Task OnPropertyChanged(object o, System.ComponentModel.PropertyChangedEventArgs e)
	{
		//is for me ?
		if (e.PropertyName == nameof(ViewModel.ToDoList))
		{
			StateHasChanged();
		}
		
	}
	void IDisposable.Dispose()
	{
		ViewModel.PropertyChanged -= async (o, e) => OnPropertyChanged(o,e);
	}
}
