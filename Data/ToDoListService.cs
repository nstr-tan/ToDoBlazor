
using Blazored.LocalStorage;
namespace BlazorToDo.Data;

public class ToDoListService
{
	const string ITEM_KEYS = "ToDoItem";

	private readonly ILocalStorageService _localStorageService;

	private List<ToDoItem> toDoItems = new();

	public ToDoListService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

	public event Action RefreshRequested;
    
	public void CallRequestRefresh()
    {
         RefreshRequested?.Invoke();
    }

	public async Task AddItemAsync(ToDoItem item)
	{
		toDoItems = await GetItemsAsync();
		toDoItems.Add(item);
        await SaveItemsAsync(toDoItems);
    }

	public async Task RemoveItemAsync(ToDoItem item)
	{
		toDoItems = await GetItemsAsync();
		ToDoItem? current = toDoItems.FirstOrDefault(todo => todo.Id == item.Id);
		if (current != null)
		{
			toDoItems.Remove(current);
			await SaveItemsAsync(toDoItems);
		}
	}

	public async Task SaveItemsAsync(List<ToDoItem> items) {
		await _localStorageService.SetItemAsync(ITEM_KEYS, items);
	}

	public async Task UpdateItemAsync(ToDoItem item) 
	{
		toDoItems = await GetItemsAsync();
		ToDoItem? current = toDoItems.FirstOrDefault(todo => todo.Id == item.Id);
		if(current != null)
		{
			int index = toDoItems.IndexOf(current);
			if(index != -1) 
			{
				toDoItems[index] = item;
			}
		}
		await SaveItemsAsync(toDoItems);
	}

	public async Task<List<ToDoItem>> GetItemsAsync()
	{
        return await _localStorageService.GetItemAsync<List<ToDoItem>>(ITEM_KEYS) ?? new();
    }

	public async Task<List<ToDoItem>> GetItemsAsync(Filter filter)
	{
		toDoItems = await _localStorageService.GetItemAsync<List<ToDoItem>>(ITEM_KEYS) ?? new();
        if(filter == Filter.Completed) 
		{
			return toDoItems.FindAll(item => item.IsCompleted == true).ToList();
		}
		else if (filter == Filter.Active)
		{
			return toDoItems.FindAll(item => item.IsCompleted == false).ToList();
		}
		else
		{
			return await GetItemsAsync();
		}
    }

	public async Task ClearCompletedAsync() 
	{
		toDoItems = await GetItemsAsync(Filter.Active);
		await SaveItemsAsync(toDoItems);
	}
}