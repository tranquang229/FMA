using FMA.API.Authorization;
using FMA.Business.Interface;
using FMA.Entities;
using FMA.Entities.Common.Responses;
using FMA.Entities.Dto;
using FMA.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IUserBiz _userBiz;

    public AccountsController(IUserBiz userBiz)
    {
        _userBiz = userBiz;
    }

    [Authorization.AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<BaseResponse<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
    {
        var response = await _userBiz.Authenticate(model);
        return new ResponseSuccess<AuthenticateResponse>(response);
    }

    [Authorization.AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var account =  await _userBiz.Register(request);
        return Ok(account);
    }

    [HttpGet]
    [Permissions(EnumPermission.AccountGetList)]
    public async Task<IActionResult> GetAll()
    {
        var users =await _userBiz.GetAll();
        return Ok(users);
    }
    
    [Roles(EnumRole.Admin)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        // only admins can access other user records
        var currentUser = (Account)HttpContext.Items[Constants.Account];
        if (id != currentUser.Id && !currentUser.Roles.Contains("Admin"))
            return Unauthorized(new { message = Constants.UNAUTHORIZED });

        var user = await _userBiz.GetById(id);
        return Ok(user);
    }
}