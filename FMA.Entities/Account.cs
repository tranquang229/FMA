using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text.Json.Serialization;
using FMA.Entities.Enum;

namespace FMA.Entities;

[Table("Accounts")]
public class Account
{
    [Key]
    public long Id { get; set; }
   
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
  
    public string Username { get; set; }
   
    public string Email { get; set; }
    public string Phone { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public List<TodoItem> TodoItems { get; set; } = new ();

    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();

}