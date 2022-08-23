using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FMA.Entities;

[Table("Accounts")]
public class Account
{
    [Key]
    public long Id { get; set; }
   
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
  
    public string Username { get; set; }
   
    public Role Role { get; set; }
   
    public string Email { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public List<TodoItem> TodoItems { get; set; } = new ();
}