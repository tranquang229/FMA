﻿using FMA.DAL.Interface;
using FMA.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FMA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
  
    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _unitOfWork.Products.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _unitOfWork.Products.GetByIdAsync(id);
        if (data == null) return Ok();
        return Ok(data);
    }
  
    [HttpPost]
    public async Task<IActionResult> Add(Product product)
    {
        var data = await _unitOfWork.Products.AddAsync(product);
        return Ok(data);
    }
  
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await _unitOfWork.Products.DeleteAsync(id);
        return Ok(data);
    }
  
    [HttpPut]
    public async Task<IActionResult> Update(Product product)
    {
        var data = await _unitOfWork.Products.UpdateAsync(product);
        return Ok(data);
    }
}