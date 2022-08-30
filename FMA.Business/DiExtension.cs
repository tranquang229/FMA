using FMA.DAL.Implement;
using FMA.DAL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FMA.Business;

public class DiExtension
{
    public static void InjectDataAccess(IServiceCollection services)
    {
        // Company
        services.AddScoped<ICompanyDataAccess, CompanyDataAccess>();

        // Account
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IAccountDataAccess, AccountDataAccess>();
    }
}