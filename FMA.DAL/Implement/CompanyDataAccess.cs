using Dapper;
using FMA.DAL.Context;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Dto;
using System.Data;
using FMA.Entities.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FMA.DAL.Implement;

public class CompanyDataAccess : ICompanyDataAccess
{
    private readonly DapperContext _context;

    public CompanyDataAccess(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        var query = "SELECT * FROM Companies";
        using (var connection = _context.CreateConnection())
        {
            var companies = await connection.QueryAsync<Company>(query);
            return companies.ToList();
        }
    }

    //public async Task<PagingResponseModel<List<Company>>> GetCompanyWithPaging(int pageNumber, int pageSize, string searchString)
    //{
    //    int maxPagSize = 50;
    //    pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

    //    int skip = (pageNumber - 1) * pageSize;
    //    int take = pageSize;

    //    string query = @"SELECT 
    //                        COUNT(*)
    //                        FROM Companies WHERE Name LIKE @SearchStr
 
    //                        SELECT  * FROM Companies WHERE Name LIKE @SearchStr
    //                        ORDER BY Id
    //                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY"
    //    ;

    //    using (var connection = _context.CreateConnection())
    //    {
    //        var reader = await connection.QueryMultipleAsync(query, new { Skip = skip, Take = take, SearchStr = "%" + searchString + "%" });

    //        int count = reader.Read<int>().FirstOrDefault();
    //        List<Company> companies = (await reader.ReadAsync<Company>()).ToList();

    //        var result = new PagingResponseModel<List<Company>>(companies, count, pageNumber, pageSize);
    //        return result;
    //    }
    //}

    public async Task<PagingResponseModel<List<Company>>> GetCompanyWithPaging(int pageNumber, int pageSize, string searchString)
    {
        using (var connection = _context.CreateConnection())
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SearchStr", searchString, DbType.String, ParameterDirection.Input);

            var reader = await connection.QueryMultipleAsync("GetListCompaniesWithPaging", parameters,commandType:CommandType.StoredProcedure);
            int count = reader.Read<int>().FirstOrDefault();
            List<Company> companies = (await reader.ReadAsync<Company>()).ToList();

            return new PagingResponseModel<List<Company>>(companies, count, pageNumber, pageSize);
        }
    }

    public async Task<Company> GetCompany(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
            return company;
        }
    }

    public async Task<Company> CreateCompany(CompanyDto company)
    {
        var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
        var parameters = new DynamicParameters();
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);
        using (var connection = _context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, parameters);
            var createdCompany = new Company
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
            return createdCompany;
        }
    }

    public async Task UpdateCompany(int id, CompanyDto company)
    {
        var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
    public async Task DeleteCompany(int id)
    {
        var query = "DELETE FROM Companies WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { id });
        }
    }

    public async Task<Company> GetCompanyByEmployeeId(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QueryFirstOrDefaultAsync<Company>("ShowCompanyForProvidedEmployeeId", parameters, commandType: CommandType.StoredProcedure);
            return company;
        }
    }


    public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @Id;" +
                    "SELECT * FROM Employees WHERE CompanyId = @Id";
        using (var connection = _context.CreateConnection())
        using (var multi = await connection.QueryMultipleAsync(query, new { id }))
        {
            var company = await multi.ReadSingleOrDefaultAsync<Company>();
            if (company != null)
                company.Employees = (await multi.ReadAsync<Employee>()).ToList();
            return company;
        }
    }

    public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
    {
        var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId";
        using (var connection = _context.CreateConnection())
        {
            var companyDict = new Dictionary<int, Company>();
            var companies = await connection.QueryAsync<Company, Employee, Company>(
                query, (company, employee) =>
                {
                    if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                    {
                        currentCompany = company;
                        companyDict.Add(currentCompany.Id, currentCompany);
                    }
                    currentCompany.Employees.Add(employee);
                    return currentCompany;
                }
            );
            return companies.Distinct().ToList();
        }
    }

    public async Task CreateMultipleCompanies(List<CompanyDto> companies)
    {
        var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var company in companies)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", company.Name, DbType.String);
                    parameters.Add("Address", company.Address, DbType.String);
                    parameters.Add("Country", company.Country, DbType.String);
                    await connection.ExecuteAsync(query, parameters, transaction: transaction);
                }
                transaction.Commit();
            }
        }
    }

    //public async Task CreateCompany(CompanyDto companies)
    //{
    //    var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";
    //    using (var connection = _context.CreateConnection())
    //    {
    //        connection.Open();
    //        using (var transaction = connection.BeginTransaction())
    //        {
    //            var listInput = new List<CompanyDto>();
    //            CreateMultipleCompanies(listInput);
    //            transaction.Commit();
    //        }
    //    }
    //}
}