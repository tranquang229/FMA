using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common.Settings;
using Microsoft.Extensions.Options;

namespace FMA.API.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSetting _jwtSetting;

    public JwtMiddleware(RequestDelegate next, IOptions<JwtSetting> jtwSetting)
    {
        _next = next;
        _jwtSetting = jtwSetting.Value;
    }

    public async Task Invoke(HttpContext context, IUserBiz userBiz, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items[Constants.Account] = await userBiz.GetById(userId.Value);
        }

        await _next(context);
    }
}