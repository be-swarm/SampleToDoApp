
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using BeSwarm.Validator;
using BeSwarm.WebApi.Models;
using CommonLibPages.Resources;
using CommonLibPages.ViewModels;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components;


namespace CommonLibPages.Pages
{

	public enum Actions
	{
		List = 1,
		Update = 2,
		Add = 3

	}

	public partial class Index : IDisposable
	{
		[CascadingParameter] BeSwarmEnvironment Session { get; set; } = default!;
		[Inject] ViewModelToDo ViewModel { get; set; } = default!;
		[Inject] NavigationManager NavigationManager { get; set; } = default!;
		[Inject] ErrorDialogService ErrorService { get; set; } = default!;
		[Inject] ConfirmDialogService DialogService { get; set; } = default!;
		private Actions _action = Actions.List;
		private CreateToDo? add;
		private UpdateToDo? update;
		private readonly ValidateContext _validatorContext = new(false);
		private ToDoRes _res = new();
		private ToDo Current = null;
		protected override async Task OnAfterRenderAsync(bool FirstTime)
		{
			if (FirstTime)
			{

				Session.EnvironmentHasChanged += async (ChangeEvents e) => await Refresh(e);
				ViewModel.PropertyChanged += async (o, e) => await Refresh(0);
				await Refresh(ChangeEvents.Lang);
			}
	
				

			await base.OnAfterRenderAsync(FirstTime);
		}

		private async Task Refresh(ChangeEvents e)
		{
			if (!ViewModel.ToDoList.Any()) await CheckError(await ViewModel.GetAllToDo()); //firstime ?
			if (e != ChangeEvents.DarkMode ) StateHasChanged();
			if (e == ChangeEvents.Lang)
			{
				_res.Culture = Session.CultureInfo;
				StateHasChanged();
			}
		}

		private async Task Cancel()
		{
			_action = Actions.List;
		}

		//
		// Add
		//
		private async Task Add()
		{
			add = new();
			add.Deadline = DateTimeOffset.Now;
			_action = Actions.Add;
		}

		private async Task ActionAdd()
		{
			var result=await ViewModel.AddToDo(add!);
			if (!result.IsOk)
			{
				await ErrorService.Show("", result.Error.Description);
			}
			_action = Actions.List;
		}

		//
		// delete
		//
		private async Task ActionDelete(ToDo del)
		{
			Current = del;
			await DialogService.Show("Question", _res.MessageDelete(), _res.Yes(), _res.No(), ConfirmDeleteToDo, null);
		}
		private async Task ConfirmDeleteToDo()
		{
			await CheckError(await ViewModel.DeleteToDo(Current));
		}
		//
		// update
		//
		private async Task Update(ToDo edit)
		{
			update = edit.ForUpdate();
			_action = Actions.Update;
		}

		private async Task ActionUpdate()
		{
			
			await CheckError( await ViewModel.UpdateToDo(update));
			_action = Actions.List;
		}

		// 
		//done
		//
		private async Task ActionDone(ToDo _done)
		{
			update = _done.ForUpdate();
			update.Closed = true;
			await CheckError( await ViewModel.UpdateToDo(update));
		}

		private async Task ActionUnDone(ToDo _done)
		{
			update = _done.ForUpdate();
			update.Closed = false;
			await CheckError(await ViewModel.UpdateToDo(update));
		}

		private async Task CheckError(ResultAction result)
		{
			if (result.IsError)
			{
				await ErrorService.Show("", result.Error.Description);
			}
		}
		void IDisposable.Dispose()
		{
			try
			{
				Session.EnvironmentHasChanged -= async (ChangeEvents e) => await Refresh(e);
				ViewModel.PropertyChanged -= async (o, e) => await Refresh(0);

			}
			catch
			{
				// ignored
			}
		}
	}

}

