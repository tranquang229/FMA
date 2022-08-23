using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.DAL.Interface;

public interface IAccountDataAccess
{
    Task<Account> Register(RegisterRequest request);
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<Account>> GetAll();
    Task<Account> GetById(long id);
}