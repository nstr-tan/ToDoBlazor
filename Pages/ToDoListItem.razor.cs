
using BlazorToDo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorToDo.Pages
{
	public partial class ToDoListItemBase : ComponentBase
	{ 
		[Parameter] 
		public RenderFragment? ChildContent { get; set;}

		[Parameter]  
		public ToDoItem? toDo { get; set; }	

		[Parameter] 
		public EventCallback<ToDoItem> OnDeleteClick { get; set; }

		[Parameter] 
		public EventCallback<ToDoItem> OnCheckBoxClick { get; set; }

		[Inject]
		ToDoListService toDoListService { get; set; }
		
		protected Boolean isEditMode = false;
		protected async void OnRemoveClicked()
		{
			if(toDo != null && toDoListService != null)
				await toDoListService.RemoveItemAsync(toDo);
			toDoListService?.CallRequestRefresh();
		}
		protected async void OnCompleteClicked()
		{
			if(toDo != null && toDoListService != null)
			{
				toDo.IsCompleted = !toDo.IsCompleted;
				await toDoListService.UpdateItemAsync(toDo);
			}
			toDoListService?.CallRequestRefresh();
		}

		protected async void OnDoubleClick() 
		{
			isEditMode = !isEditMode;
		}
		protected async void OnKeyDown(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter")
			{
				if(toDo != null && toDoListService != null)
					await toDoListService.UpdateItemAsync(toDo);
				toDoListService?.CallRequestRefresh();
				isEditMode = false;
			}
		}
	}
}