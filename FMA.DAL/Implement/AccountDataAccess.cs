using AutoMapper;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Dto;
using Microsoft.AspNetCore.Identity;

namespace FMA.DAL.Implement
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountDataAccess(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
        }

        public async Task<bool> RegisterAccount(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                    }
                    return false;
                }

                await _userManager.AddToRolesAsync(user, userDto.Roles);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
