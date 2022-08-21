using FMA.Entities.Dto;

namespace FMA.DAL.Interface
{
    public interface IAccountDataAccess
    {
        Task<bool> RegisterAccount(UserDto userDto);
    }
}
