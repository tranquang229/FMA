using FMA.Entities.Dto;

namespace FMA.DAL.Interface
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDto userDto);

        Task<string> CreateToken();
    }
}
