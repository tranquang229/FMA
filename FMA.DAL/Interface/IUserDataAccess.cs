using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.DAL.Interface;

public interface IUserDataAccess
{
    Task<long> Register(RegisterRequest request);
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<Account>> GetAll();
    Task<Account> GetById(long id);
}