using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Dto;
using FMA.Business.Interface;

namespace FMA.Business.Implements;

public class CompanyBiz : ICompanyBiz
{
    private readonly ICompanyDataAccess _companyDataAccess;

    public CompanyBiz(ICompanyDataAccess companyDataAccess)
    {
        _companyDataAccess = companyDataAccess;
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        return await _companyDataAccess.GetCompanies();
    }

    public async Task<Company> GetCompany(int id)
    {
        return await _companyDataAccess.GetCompany(id);
    }

    public async Task<Company> CreateCompany(CompanyDto company)
    {
        return await _companyDataAccess.CreateCompany(company);
    }

    public async Task UpdateCompany(int id, CompanyDto company)
    {
        await _companyDataAccess.UpdateCompany(id, company);
    }

    public async Task DeleteCompany(int id)
    {
        await _companyDataAccess.DeleteCompany(id);
    }

    public async Task<Company> GetCompanyByEmployeeId(int id)
    {
        return await _companyDataAccess.GetCompanyByEmployeeId(id);
    }

    public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
    {
        return await _companyDataAccess.GetCompanyEmployeesMultipleResults(id);
    }

    public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
    {
        return await _companyDataAccess.GetCompaniesEmployeesMultipleMapping();
    }

    public async Task CreateMultipleCompanies(List<CompanyDto> companies)
    {
        await _companyDataAccess.CreateMultipleCompanies(companies);
    }
}