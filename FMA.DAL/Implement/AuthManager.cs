using FMA.DAL.Interface;
using FMA.Entities.Dto;

namespace FMA.DAL.Implement
{
    public class AuthManager : IAuthManager
    {
        public Task<bool> ValidateUser(LoginUserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }
    }
}
