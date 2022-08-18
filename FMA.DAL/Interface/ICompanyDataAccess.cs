using FMA.Entities;
using FMA.Entities.Dto;

namespace FMA.DAL.Interface;

public interface ICompanyDataAccess 
{
    public Task<IEnumerable<Company>> GetCompanies();
        
    public Task<Company> GetCompany(int id);
       
    public Task<Company> CreateCompany(CompanyDto company);
      
    public Task UpdateCompany(int id, CompanyDto company);
     
    public Task DeleteCompany(int id);
      
    public Task<Company> GetCompanyByEmployeeId(int id);

    public Task<Company> GetCompanyEmployeesMultipleResults(int id);

    public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();

    public Task CreateMultipleCompanies(List<CompanyDto> companies);
}