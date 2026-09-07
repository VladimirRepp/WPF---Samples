/// <summary>
/// Отдельный класс для представления элемента списка дел (ToDoItem)
/// </summary>
public class TodoItem
{
    public string Title { get; set; }

    public bool IsCompleted { get; set; }

    public TodoItem(string title)
    {
        Title = title;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return IsCompleted
            ? $"(+) {Title}"
            : $"( ) {Title}";
    }
}