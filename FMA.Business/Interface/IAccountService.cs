using FMA.Entities.Dto;

namespace FMA.Business.Interface
{
    public interface IAccountService
    {
        Task<bool> RegisterAccount(UserDto userDto);
    }
}
