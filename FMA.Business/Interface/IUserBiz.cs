using FMA.Entities;
using FMA.Entities.Dto;
using FMA.Entities.Enum;

namespace FMA.Business.Interface;

public interface IUserBiz
{
    Task<Account> Register(RegisterRequest request);
    
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
   
    Task<IEnumerable<Account>> GetAll();
   
    Task<Account> GetById(long id);

    Task<List<Role>> GetRolesFromAccount(long accountId);
 
    Task<List<Permission>> GetFullPermissionFromAccountId(long accountId);


    //Task<EnumRole> GetAccountRoles(Account account) 
}