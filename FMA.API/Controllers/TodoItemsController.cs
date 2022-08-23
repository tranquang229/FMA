using FMA.API.Authorization;
using FMA.Business.Implements;
using FMA.Business.Interface;
using FMA.Entities;
using FMA.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController
    {
        private readonly ITodoItemBiz _todoItemBiz;

        public TodoItemsController(ITodoItemBiz todoItemBiz)
        {
            _todoItemBiz = todoItemBiz;
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            try
            {
                var todoItems = await _todoItemBiz.GetAllTodoItems();
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
       
        
        [Authorize(Role.Admin, Role.User)]
        [HttpGet]
        public async Task<IActionResult> GetTodoItemsWithPaging(int pageNumber, int pageSize, string searchStr)
        {
            try
            {
                var todoItems = await _todoItemBiz.GetTodoItemWithPaging(pageNumber, pageSize, searchStr);
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("{id}", Name = "TodoItemById")]
        public async Task<IActionResult> GetTodoItem(int id)
        {
            try
            {
                var todoItem = await _todoItemBiz.GetTodoItem(id);
                if (todoItem == null)
                    return NotFound();
                return Ok(todoItem);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(TodoItemDto todoItem)
        {
            try
            {
                todoItem.AccountId = CurrentAccount.Id;

                var createdTodoItem = await _todoItemBiz.CreateTodoItem(todoItem);
                return CreatedAtRoute("TodoItemById", new { id = createdTodoItem.Id }, createdTodoItem);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDto todoItem)
        {
            try
            {
                await _todoItemBiz.UpdateTodoItem(id, todoItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            try
            {
                await _todoItemBiz.DeleteTodoItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("ByAccountId/{id}")]
        public async Task<IActionResult> GetCompanyForAccount(long id)
        {
            try
            {
                var company = await _todoItemBiz.GetTodoItemByAccountId(id);
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

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("{id}/MultipleResult")]
        public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        {
            throw new Exception();
        }


        [Authorize(Role.Admin, Role.User)]
        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
        {
            throw new Exception();
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpPost("CreateMultipleCompanies")]
        public async Task<IActionResult> CreateMultipleCompanies(List<CompanyDto> list)
        {
            throw new Exception();
        }

    }
}
