using AutoMapper;
using Todo.Application.Models;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Infrastructure.Database.Repositories
{
    public class TodoRepositoryProfile : Profile
    {
        public TodoRepositoryProfile()
        {
            CreateMap<TodoItem, TodoItemEntity>();
            CreateMap<TodoItemEntity, TodoItem>();
        }
    }
}