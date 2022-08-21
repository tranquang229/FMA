using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities.Dto;

namespace FMA.Business.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataAccess _accountDataAccess;

        public AccountService(IAccountDataAccess accountDataAccess)
        {
            _accountDataAccess = accountDataAccess;
        }

        public Task<bool> RegisterAccount(UserDto userDto)
        {
            return _accountDataAccess.RegisterAccount(userDto);
        }
    }
}
