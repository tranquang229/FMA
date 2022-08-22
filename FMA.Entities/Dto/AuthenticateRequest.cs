using System.ComponentModel.DataAnnotations;

namespace FMA.Entities.Dto;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}