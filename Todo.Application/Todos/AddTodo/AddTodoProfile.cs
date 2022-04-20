using AutoMapper;
using Todo.Application.Models;

namespace Todo.Application.Todos.AddTodo
{
    /// <summary>
    /// Профиль маппинга для <see cref="AddTodoHandler"/>
    /// </summary>
    public class AddTodoProfile : Profile
    {
        public AddTodoProfile()
        {
            CreateMap<AddTodoCommand, TodoItem>()
                .ForMember(c => c.IsComplete, cfg => cfg.MapFrom(c => c.IsComplete))
                .ForMember(c => c.Name,cfg => cfg.MapFrom(c => c.Name))
                .ForMember( c => c.Id, cfg => cfg.Ignore());
        }
    }
}