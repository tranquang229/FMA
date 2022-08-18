using FMA.DAL.Interface;

namespace FMA.DAL.Implement;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IProductDataAccess productRepository)
    {
        Products = productRepository;
    }

    public IProductDataAccess Products { get; }
}