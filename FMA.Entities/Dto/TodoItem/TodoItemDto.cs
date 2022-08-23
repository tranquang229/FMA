using System.ComponentModel.DataAnnotations;

namespace FMA.Entities.Dto.TodoItem;

public class TodoItemDto
{
    [Key]
    public long Id { get; set; }
    public string Content { get; set; }

    public bool? IsDone { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public long AccountId { get; set; }
}