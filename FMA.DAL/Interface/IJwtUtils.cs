using FMA.Entities;

namespace FMA.DAL.Interface;

public interface IJwtUtils
{
    public Task<string> GenerateJwtToken(Account account);
    public long? ValidateJwtToken(string token);
}