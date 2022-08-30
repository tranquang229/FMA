using FMA.API.Utils;
using FMA.Entities;
using FMA.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Extensions;

namespace FMA.API.Authorization;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class PermissionsAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<EnumPermission> _permissions;

    public PermissionsAttribute(params EnumPermission[] permissions)
    {
        _permissions = permissions ?? new EnumPermission[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var permissionsCheck = context.ActionDescriptor.EndpointMetadata.OfType<PermissionsAttribute>().Any();
        if (permissionsCheck)
        {
            var account = (Account)context.HttpContext.Items[Constants.Account];
            if (account != null)
            {
                var listAccountPermissions = account.Permissions.Select(x => x.GetEnumFromDisplayName<EnumPermission>()).ToList();
                if (_permissions.Any() && !ListUtils<EnumPermission>.CheckListContain(listAccountPermissions, _permissions))
                {
                    context.Result = new JsonResult(new { message = Constants.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}