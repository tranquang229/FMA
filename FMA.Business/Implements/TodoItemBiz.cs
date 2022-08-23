using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common;
using FMA.Entities.Dto;

namespace FMA.Business.Implements;

public class TodoItemBiz:ITodoItemBiz
{
    private readonly ITodoItemDataAccess _todoItemDataAccess;

    public TodoItemBiz(ITodoItemDataAccess todoItemDataAccess)
    {
        _todoItemDataAccess = todoItemDataAccess;
    }

    public  async Task<IEnumerable<TodoItem>> GetAllTodoItems()
    {
        return await _todoItemDataAccess.GetAllTodoItems();
    }

    public async Task<PagingResponseModel<List<TodoItem>>> GetTodoItemWithPaging(int pageNumber, int pageSize, string searchStr)
    {
        return await _todoItemDataAccess.GetTodoItemWithPaging(pageNumber, pageSize, searchStr);
    }

    public async Task<TodoItem> GetTodoItem(long id)
    {
        return await _todoItemDataAccess.GetTodoItem(id);
    }

    public async Task<TodoItem> CreateTodoItem(TodoItemDto todoItem)
    {
        return await _todoItemDataAccess.CreateTodoItem(todoItem);
    }

    public async Task UpdateTodoItem(long id, TodoItemDto todoItem)
    {
        await _todoItemDataAccess.UpdateTodoItem(id, todoItem);
    }

    public async Task DeleteTodoItem(long id)
    {
        await _todoItemDataAccess.DeleteTodoItem(id);
    }

    public async Task<TodoItem> GetTodoItemByAccountId(long id)
    {
        return await _todoItemDataAccess.GetTodoItemByAccountId(id);
    }

    public async Task<TodoItem> GetTodoItemEmployeesMultipleResults(long id)
    {
        return await _todoItemDataAccess.GetTodoItemEmployeesMultipleResults(id);
    }

    public async Task<List<TodoItem>> GetCompaniesEmployeesMultipleMapping()
    {
        return await _todoItemDataAccess.GetCompaniesEmployeesMultipleMapping();
    }

    public async Task CreateMultipleTodoItems(List<TodoItemDto> todoItems)
    {
        await _todoItemDataAccess.CreateMultipleTodoItems(todoItems);
    }
}