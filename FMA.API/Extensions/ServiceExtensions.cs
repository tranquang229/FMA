using FMA.Business;
using FMA.Business.Implements;
using FMA.Business.Interface;
using FMA.Entities;
using FMA.Entities.Common.Settings;

namespace FMA.API.Extensions;

public static class ServiceExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICompanyBiz, CompanyBiz>();
        services.AddScoped<IUserBiz, UserBiz>();

        DiExtension.InjectDataAccess(services);
    }

    public static void AddConfigSettings(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<JwtSetting>(builder.Configuration.GetSection(Constants.JwtSetting));
    }
}