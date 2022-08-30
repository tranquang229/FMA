using FMA.API.Extensions;
using FMA.API.Utils;
using FMA.Entities;
using FMA.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FMA.API.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<EnumRole> _roles;

    public RolesAttribute(params EnumRole[] roles)
    {
        _roles = roles ?? new EnumRole[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // roles
        var rolesCheck = context.ActionDescriptor.EndpointMetadata.OfType<RolesAttribute>().Any();
        if (rolesCheck)
        {
            var account = (Account)context.HttpContext.Items[Constants.Account];
            if (account != null)
            {
                var listAccountRoles = account.Roles.Select(x=> x.GetValueFromDescription<EnumRole>()).ToList();
                if (_roles.Any() && !ListUtils<EnumRole>.CheckListContain(listAccountRoles, _roles))
                {
                    context.Result = new JsonResult(new { message = Constants.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}