using System.Text.Json.Serialization;

namespace FMA.Entities;

public class Account
{
    public long Id { get; set; }
   
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
  
    public string Username { get; set; }
   
    public string Email { get; set; }
  
    public string Phone { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public List<string> Roles { get; set; } = new();
 
    public List<string> Permissions { get; set; } = new();
}