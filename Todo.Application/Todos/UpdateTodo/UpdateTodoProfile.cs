using AutoMapper;
using Todo.Application.Models;

namespace Todo.Application.Todos.UpdateTodo
{
    /// <summary>
    /// Профиль маппинга <see cref="UpdateTodoHandler"/>
    /// </summary>
    public class UpdateTodoProfile : Profile
    {
        public UpdateTodoProfile()
        {
            CreateMap<UpdateTodoCommand, TodoItem>();
        }
    }
}