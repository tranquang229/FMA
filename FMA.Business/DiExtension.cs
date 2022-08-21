using FMA.DAL.Implement;
using FMA.DAL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FMA.Business;

public class DiExtension
{
    public static void InjectDataAccess(IServiceCollection services)
    {
        services.AddTransient<IProductDataAccess, ProductDataAccess>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICompanyDataAccess, CompanyDataAccess>();
        services.AddScoped<IAccountDataAccess, AccountDataAccess>();
    }
}