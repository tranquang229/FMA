using FMA.Entities.Common;
using FMA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Entities.Dto.TodoItem;

namespace FMA.DAL.Interface
{
    public interface ITodoItemDataAccess
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
}
