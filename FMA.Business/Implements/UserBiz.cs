using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.Business.Implements;

public class UserBiz : IUserBiz
{
    private readonly IAccountDataAccess _userDataAccess;

    public UserBiz(IAccountDataAccess userDataAccess)
    {
        _userDataAccess = userDataAccess;
    }

    public async Task<Account> Register(RegisterRequest request)
    {
        return await _userDataAccess.Register(request);
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        return await _userDataAccess.Authenticate(model);
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _userDataAccess.GetAll();
    }

    public async Task<Account> GetById(long accountId)
    {
        return await _userDataAccess.GetById(accountId);
    }

    public async Task<List<Role>> GetRolesFromAccount(long accountId)
    {
        return await _userDataAccess.GetRolesFromAccount(accountId);
    }

    public async Task<List<Permission>> GetFullPermissionFromAccountId(long accountId)
    {
        return await _userDataAccess.GetFullPermissionFromAccountId(accountId);
    }
}