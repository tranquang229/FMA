using AutoMapper;
using FMA.Entities;
using FMA.Entities.Dto.TodoItem;

namespace FMA.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        TodoItemMap();
    }
       
    private void TodoItemMap()
    {
        CreateMap<TodoItemDto, TodoItem>().ReverseMap();
    }
}