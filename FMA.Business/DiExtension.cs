using FMA.DAL.Implement;
using FMA.DAL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FMA.Business;

public class DiExtension
{
    public static void InjectDataAccess(IServiceCollection services)
    {
        // Product
        services.AddTransient<IProductDataAccess, ProductDataAccess>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        // Company
        services.AddScoped<ICompanyDataAccess, CompanyDataAccess>();

        // Account
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IAccountDataAccess, AccountDataAccess>();

        // TodoItem
        services.AddScoped<ITodoItemDataAccess, TodoItemDataAccess>();
    }
}