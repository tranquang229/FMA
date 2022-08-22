using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.Business.Implements;

public class UserBiz : IUserBiz
{
    private readonly IUserDataAccess _userDataAccess;

    public UserBiz(IUserDataAccess userDataAccess)
    {
        _userDataAccess = userDataAccess;
    }

    public async Task<long> Register(RegisterRequest request)
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

    public async Task<Account> GetById(long id)
    {
        return await _userDataAccess.GetById(id);
    }
}