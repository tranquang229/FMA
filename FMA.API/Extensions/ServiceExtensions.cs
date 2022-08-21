using FMA.Business;
using FMA.Business.Implements;
using FMA.Business.Interface;

namespace FMA.API.Extensions;

public static class ServiceExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICompanyBiz, CompanyBiz>();
        services.AddScoped<IAccountService, AccountService>();
        DiExtension.InjectDataAccess(services);
    }
}