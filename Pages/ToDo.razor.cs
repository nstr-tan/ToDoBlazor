using BlazorToDo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorToDo.Pages
{
	public partial class ToDoBase : ComponentBase
	{
		protected List<ToDoItem> toDoList = new();
		protected List<ToDoItem> filteredToDoList = new();
		protected string newToDo = "";
		protected Filter currentFilter = Filter.All;
		protected Boolean allCompleted = false;

		[Inject]
		ToDoListService toDoListService { get; set; }
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				toDoListService.RefreshRequested += RefreshData;
				toDoList = await toDoListService.GetItemsAsync();
				IsAllCompleted();
				StateHasChanged();
			}
		}  
		protected async void AddToDo()
		{
			if (!string.IsNullOrWhiteSpace(newToDo) && toDoListService != null)
			{
				await toDoListService.AddItemAsync(new ToDoItem { Title = newToDo, IsCompleted = false });
				newToDo = string.Empty;
				RefreshData();
			}
		}
		public void OnKeyDown(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter")
			{
				AddToDo();
			}
		}

		public int TotalItemsLeft()
		{
			int itemsLeft = 0;
			itemsLeft = toDoList.FindAll(item => item.IsCompleted == false).ToList().Count;
			return itemsLeft;
		}
		public string ItemLeftText() 
		{
			string itemsLeftText = "";
			int itemsLeft = TotalItemsLeft();

			if(itemsLeft == 1) 
			{
				itemsLeftText = itemsLeft + " item left";
			} 
			else if(toDoList.Count > 0 && itemsLeft != 1) 
			{
				itemsLeftText = itemsLeft + " items left";
			}

			return itemsLeftText;
		}


		protected async void OnFilterClicked(Filter filter) 
		{
			filteredToDoList = await toDoListService.GetItemsAsync(filter);
			currentFilter = filter;
			StateHasChanged();
		}
		protected async void RefreshData()
		{
			toDoList = await toDoListService.GetItemsAsync();
			StateHasChanged();
		}

		protected async void OnClearCompleted() 
		{
			await toDoListService.ClearCompletedAsync();
			OnFilterClicked(currentFilter);
			RefreshData();
		}

		protected async void OnToggleAllClicked() 
		{
			Boolean isAllCompleted = IsAllCompleted();
			foreach(var toDo in toDoList) 
			{
				toDo.IsCompleted = !isAllCompleted;
			}
			await toDoListService.SaveItemsAsync(toDoList);
			StateHasChanged();
		}

		protected Boolean IsAllCompleted()
		{
			if(toDoList.Count > 0) {
				allCompleted = true;
				foreach(var toDo in toDoList) 
				{
					if(toDo.IsCompleted == false)
					{
						allCompleted = false;
					}
				}
			}
			else 
				allCompleted = false;
			
			return allCompleted;
		}

		protected Boolean HasCompleted() 
		{	
			Boolean hasCompleted = false;
			if(toDoList.Count > 0) {
				
				foreach(var todo in toDoList) 
				{
					if(todo.IsCompleted == true)
					{
						hasCompleted = true;
						break;
					}
				}
			}
			else 
				hasCompleted = false;
			
			return hasCompleted;
		}
	}
}