using FMA.Entities;
using FMA.Entities.Common;
using FMA.Entities.Dto;

namespace FMA.Business.Interface;

public interface ICompanyBiz
{
    public Task<IEnumerable<Company>> GetCompanies();

    public Task<PagingResponseModel<List<Company>>> GetCompanyWithPaging(int pageNumber, int pageSize, string searchStr);

    public Task<Company> GetCompany(int id);

    public Task<Company> CreateCompany(CompanyDto company);

    public Task UpdateCompany(int id, CompanyDto company);

    public Task DeleteCompany(int id);

    public Task<Company> GetCompanyByEmployeeId(int id);

    public Task<Company> GetCompanyEmployeesMultipleResults(int id);

    public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();

    public Task CreateMultipleCompanies(List<CompanyDto> companies);
}