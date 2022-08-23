using FMA.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FMA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected Account CurrentAccount
    {
        get
        {
            Account currentAccount = null;

            try
            {
                currentAccount = (Account)HttpContext.Items[Constants.Account];
            }
            catch (Exception e)
            {
                
            }

            return currentAccount;
        }
    }

    protected Guid CurrentAccountId
    {
        get
        {
            var currentUserId = Guid.NewGuid();

            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst(Constants.UId).Value;
                currentUserId = Guid.Parse(userId);
            }
            catch (Exception e)
            {
                
            }

            return currentUserId;
        }
    }
}