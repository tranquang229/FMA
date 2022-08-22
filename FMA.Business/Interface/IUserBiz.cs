using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.Business.Interface;

public interface IUserBiz
{
    Task<long> Register(RegisterRequest request);
    
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
   
    Task<IEnumerable<Account>> GetAll();
   
    Task<Account> GetById(long id);
}