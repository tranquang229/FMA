namespace FMA.Entities.Dto;

public class AuthenticateResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Permissions { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(Account user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Token = token;
        Roles = user.Roles;
        Permissions = user.Permissions;
    }
}