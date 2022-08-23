using FMA.Entities.Common;
using FMA.Entities;
using FMA.Entities.Dto.TodoItem;

namespace FMA.Business.Interface;

public interface ITodoItemBiz
{
    public Task<IEnumerable<TodoItem>> GetAllTodoItems();
    public Task<IEnumerable<TodoItemWithAccountDto>> GetAllTodoItemsV2();

    public Task<PagingResponseModel<List<TodoItem>>> GetTodoItemWithPaging(int pageNumber, int pageSize, string searchStr);

    public Task<TodoItem> GetTodoItem(long id);

    public Task<TodoItem> CreateTodoItem(TodoItemDto todoItem);

    public Task UpdateTodoItem(long id, TodoItemDto todoItem);

    public Task DeleteTodoItem(long id);

    public Task<TodoItem> GetTodoItemByAccountId(long id);

    public Task<TodoItem> GetTodoItemEmployeesMultipleResults(long id);

    public Task<List<TodoItem>> GetCompaniesEmployeesMultipleMapping();

    public Task CreateMultipleTodoItems(List<TodoItemDto> todoItems);
}