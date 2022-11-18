
using BeSwarm.WebApi;

using CommunityToolkit.Mvvm.Input;

using System.Collections.Immutable;
using System.Text;
using BeSwarm.WebApi.Models;
using BeSwarm.WebApi;

namespace CommonLibPages.ViewModels
{

	public class ToDo
	{
		public bool Closed { get; }
		public DateTimeOffset Deadline { get; }
		public string Description { get; }
		public string Picture { get;  }
		public string Uid { get;  }

		public ToDo(string description, DateTimeOffset deadline, string picture, string uid, bool closed)
		{
			Description = description;
			Deadline = deadline;
			Picture = picture;
			Uid = uid;
			Closed = closed;
		}
		public ToDo(RetToDo src)
		{
			Description = src.Description;
			Deadline = src.Deadline;
			Picture = src.Picture;
			Uid = src.Uid;
			Closed = src.Closed;
		}
		public UpdateToDo ForUpdate()
		{

			UpdateToDo ret = new()
			{
				Description = Description,
				Deadline = Deadline,
				Picture = Picture,
				Uid = Uid,
				Closed = Closed,
			};
			return ret;
		}
	}

	public partial class ViewModelToDo : ObservableObject
	{
		private readonly SessionWebApi _session;
		private readonly List<ToDo> _todos = new();


		public IEnumerable<ToDo> ToDoList { get; private set; }

		public ViewModelToDo(SessionWebApi session)
		{
			_session = session;
			ToDoList = _todos;
		}

		private void BuildToDoList()
		{
			_todos.Sort((a, b) => a.Deadline.CompareTo(b.Deadline));
			ToDoList =_todos;
			OnPropertyChanged(nameof(ToDoList));
		}
		[RelayCommand]
		public async Task<ResultAction> GetAllToDo()
		{
			ResultAction res = new();
			if (_session.SessionIsActive == false) return res;
			Planner planner = new("", _session.GetUserHttpClient());
			try
			{
				var gestures = await planner.GetToDoListAsync(_session.UserToken);
				if (gestures.Status == StatusAction.Ok)
				{
					_todos.Clear();
					foreach (var item in gestures.Datas)
					{
						_todos.Add(new(item));

					}
					BuildToDoList();
				}
			}
			catch (Exception exception)
			{
				res = _session.GetInternalErrorFromException(exception);
			}
			return res;
		}
		[RelayCommand]
		public async Task<ResultAction> AddToDo(CreateToDo add)
		{
			ResultAction res = new();
			Planner planner = new("", _session.GetUserHttpClient());
			try
			{
				var statusadd = await planner.AddToDoAsync(_session.UserToken, add);
				if (statusadd.Status == StatusAction.Ok)
				{
					_todos.Add(new(add.Description, add.Deadline, add.Picture, statusadd.Datas.Uid, add.Closed));
					BuildToDoList();
				}
			}
			catch (Exception exception)
			{
				res = _session.GetInternalErrorFromException(exception);
			}

			return res;
		}
		[RelayCommand]
		public async Task<ResultAction> UpdateToDo(UpdateToDo updated)
		{

		    ResultAction res = new();
			Planner planner = new("", _session.GetUserHttpClient());
			try
			{
				
				var statusadd = await planner.UpdateToDoAsync(_session.UserToken, updated);
				if (statusadd.Status == StatusAction.Ok)
				{
					_todos.Remove(_todos.Where(x => x.Uid == updated.Uid).FirstOrDefault());
					_todos.Add(new(updated.Description, updated.Deadline, updated.Picture, updated.Uid, updated.Closed));
					BuildToDoList();
				}
			}
			catch (Exception exception)
			{
				res = _session.GetInternalErrorFromException(exception);
			}

			return res;
		}
		[RelayCommand]
		public async Task<ResultAction> DeleteToDo(ToDo deleted)
		{
			ResultAction res = new();
			Planner planner = new("", _session.GetUserHttpClient());
			try
			{
				var statusdel = await planner.DeleteToDoAsync(_session.UserToken, deleted.Uid);
				if (statusdel.Status == StatusAction.Ok)
				{
					_todos.Remove(_todos.Where(x => x.Uid == deleted.Uid).FirstOrDefault());
					BuildToDoList();
				}
			}
			catch (Exception exception)
			{
				res = _session.GetInternalErrorFromException(exception);
			}
			return res;
		}
	

	}
}
