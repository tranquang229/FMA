using System.Text.Json.Serialization;

namespace FMA.Entities.Dto;

public class AccountDto
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }

    public string Email { get; set; }

    public List<Entities.TodoItem> TodoItems { get; set; } = new();
}