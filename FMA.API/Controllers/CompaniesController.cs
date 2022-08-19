using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMA.Entities.Dto;
using FMA.Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyBiz _companyBiz;

    public CompaniesController(ICompanyBiz companyBiz)
    {
        _companyBiz = companyBiz;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await _companyBiz.GetCompanies();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCompaniesWithPaging(int pageNumber, int pageSize, string searchStr)
    {
        try
        {
            var companies = await _companyBiz.GetCompanyWithPaging(pageNumber, pageSize, searchStr);
            return Ok(companies);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet("{id}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(int id)
    {
        try
        {
            var company = await _companyBiz.GetCompany(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyDto company)
    {
        try
        {
            var createdCompany = await _companyBiz.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, CompanyDto company)
    {
        try
        {
            var dbCompany = await _companyBiz.GetCompany(id);
            if (dbCompany == null)
                return NotFound();
            await _companyBiz.UpdateCompany(id, company);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var dbCompany = await _companyBiz.GetCompany(id);
            if (dbCompany == null)
                return NotFound();
            await _companyBiz.DeleteCompany(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("ByEmployeeId/{id}")]
    public async Task<IActionResult> GetCompanyForEmployee(int id)
    {
        try
        {
            var company = await _companyBiz.GetCompanyByEmployeeId(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
        
    [HttpGet("{id}/MultipleResult")]
    public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
    {
        try
        {
            var company = await _companyBiz.GetCompanyEmployeesMultipleResults(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("MultipleMapping")]
    public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
    {
        try
        {
            var company = await _companyBiz.GetCompaniesEmployeesMultipleMapping();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("CreateMultipleCompanies")]
    public async Task<IActionResult> CreateMultipleCompanies(List<CompanyDto> list)
    {
        try
        {
            await _companyBiz.CreateMultipleCompanies(list);
            return Ok(list);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}