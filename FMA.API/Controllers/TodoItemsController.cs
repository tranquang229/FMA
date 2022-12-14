using AutoMapper;
using FMA.API.Authorization;
using FMA.Business.Implements;
using FMA.Business.Interface;
using FMA.Entities;
using FMA.Entities.Dto;
using FMA.Entities.Dto.TodoItem;
using FMA.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController
    {
        private readonly ITodoItemBiz _todoItemBiz;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemBiz todoItemBiz, IMapper mapper)
        {
            _todoItemBiz = todoItemBiz;
            _mapper = mapper;
        }

        [Authorize(Policy = "Manager")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            try
            {
                var todoItems = await _todoItemBiz.GetAllTodoItems();
                return Ok(_mapper.Map<IEnumerable<TodoItemDto>>(todoItems));
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        [Roles(EnumRole.Admin, EnumRole.SuperAdmin)]
        [HttpGet("allv2")]
        public async Task<IActionResult> GetAllTodoItemsV2()
        {
            try
            {
                var todoItems = await _todoItemBiz.GetAllTodoItemsV2();
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
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

        [Roles(EnumRole.Admin, EnumRole.User)]
        [HttpGet("{id}/MultipleResult")]
        public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        {
            throw new Exception();
        }


        [Roles(EnumRole.Admin, EnumRole.User)]
        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
        {
            throw new Exception();
        }

        [Roles(EnumRole.Admin)]
        [HttpPost("CreateMultipleCompanies")]
        public async Task<IActionResult> CreateMultipleCompanies(List<CompanyDto> list)
        {
            throw new Exception();
        }

    }
}
