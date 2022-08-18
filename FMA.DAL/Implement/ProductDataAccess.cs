using Dapper;
using FMA.DAL.Interface;
using FMA.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace FMA.DAL.Implement;

public class ProductDataAccess : IProductDataAccess
{
    private readonly IConfiguration _configuration;
 
    public ProductDataAccess(IConfiguration configuration)
    {
      _configuration = configuration;
    }
   
    public async Task<int> AddAsync(Product entity)
    {
        entity.AddedOn = DateTime.Now;
        var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Products WHERE Id = @Id";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result;
        }
    }
    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        var sql = "SELECT * FROM Products";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QueryAsync<Product>(sql);
            return result.ToList();
        }
    }
    public async Task<Product> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Products WHERE Id = @Id";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
            return result;
        }
    }
    public async Task<int> UpdateAsync(Product entity)
    {
        entity.ModifiedOn = DateTime.Now;
        var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
}