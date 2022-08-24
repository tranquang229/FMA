using FMA.Business.Interface;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common;
using FMA.Entities.Dto.TodoItem;

namespace FMA.Business.Implements;

public class TodoItemBiz : ITodoItemBiz
{
    private readonly ITodoItemDataAccess _todoItemDataAccess;
    private readonly IAccountDataAccess _accountDataAccess;

    public TodoItemBiz(ITodoItemDataAccess todoItemDataAccess, IAccountDataAccess accountDataAccess)
    {
        _todoItemDataAccess = todoItemDataAccess;
        _accountDataAccess = accountDataAccess;
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
    {
        var account = await _accountDataAccess.GetById(36);
        return await _todoItemDataAccess.GetAllTodoItems();
    }
    public async Task<IEnumerable<TodoItemWithAccountDto>> GetAllTodoItemsV2()
    {
        return await _todoItemDataAccess.GetAllTodoItemsV2();
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