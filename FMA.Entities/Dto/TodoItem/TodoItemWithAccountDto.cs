namespace FMA.Entities.Dto.TodoItem;

public class TodoItemWithAccountDto
{
    public long Id { get; set; }
    public string Content { get; set; }

    public bool? IsDone { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public Account Account { get; set; }
}