namespace BlazorToDo.Data;
	public class ToDoItem
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Title { get; set; } = string.Empty;

		public Boolean IsCompleted { get; set; } = false;
	}

 public enum Filter
    {
        All = 1,
        Active,
        Completed

    }