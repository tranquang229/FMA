using Dapper;
using FMA.DAL.Context;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common;
using FMA.Entities.Dto;
using FMA.Entities.Dto.TodoItem;

namespace FMA.DAL.Implement;

public class TodoItemDataAccess : ITodoItemDataAccess
{
    private readonly DapperContext _context;

    public TodoItemDataAccess(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
    {
        var query = "SELECT * FROM TodoItems";
        using (var connection = _context.CreateConnection())
        {
            var todoItems = await connection.QueryAsync<TodoItem>(query);
            return todoItems.ToList();
        }
    }

    public async Task<IEnumerable<TodoItemWithAccountDto>> GetAllTodoItemsV2()
    {
        string sql = "Select * FROM TodoItems ti JOIN Accounts acc ON ti.AccountId = acc.Id";
        using var connection = _context.CreateConnection();
        var todoItems = await connection.QueryAsync<TodoItem, Account, TodoItemWithAccountDto>(sql, (todoItem, acc) =>
            new TodoItemWithAccountDto
            {
                Account = acc,
                Content = todoItem.Content,
                CreatedDate = todoItem.CreatedDate,
                Id = todoItem.Id,
                IsDone = todoItem.IsDone,
                UpdatedDate = todoItem.UpdatedDate
            });
        return todoItems;
    }



    public Task<PagingResponseModel<List<TodoItem>>> GetTodoItemWithPaging(int pageNumber, int pageSize, string searchStr)
    {
        throw new NotImplementedException();
    }

    public async Task<TodoItem> GetTodoItem(long id)
    {
        var query = "SELECT * FROM TodoItems WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var todoItem = await connection.QuerySingleOrDefaultAsync<TodoItem>(query, new { id });
            return todoItem;
        }
    }

    public async Task<TodoItem> CreateTodoItem(TodoItemDto todoItem)
    {
        using (var connection = _context.CreateConnection())
        {
            var newTodoItem = new TodoItem()
            {
                Content = todoItem.Content,
                IsDone = todoItem.IsDone,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = null,
                AccountId = todoItem.AccountId
            };
            var id = await connection.InsertAsync<long, TodoItem>(newTodoItem);
            newTodoItem.Id = id;
          
            return newTodoItem;
        }
    }

    public async Task UpdateTodoItem(long id, TodoItemDto todoItem)
    {
        using (var connection = _context.CreateConnection())
        {
            var updateTodoItem = new TodoItem()
            {
                Id = id,
                Content = todoItem.Content,
                IsDone = todoItem.IsDone,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = null,
                AccountId = todoItem.AccountId
            };
            await connection.UpdateAsync(updateTodoItem);
        }
    }

    public async Task DeleteTodoItem(long id)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.DeleteAsync(id);
        }
    }

    public Task<TodoItem> GetTodoItemByAccountId(long accountId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> GetTodoItemEmployeesMultipleResults(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TodoItem>> GetCompaniesEmployeesMultipleMapping()
    {
        throw new NotImplementedException();
    }

    public Task CreateMultipleTodoItems(List<TodoItemDto> todoItems)
    {
        throw new NotImplementedException();
    }
}