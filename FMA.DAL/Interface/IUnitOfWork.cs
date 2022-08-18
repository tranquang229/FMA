namespace FMA.DAL.Interface;

public interface IUnitOfWork
{
    IProductDataAccess Products { get; }
}