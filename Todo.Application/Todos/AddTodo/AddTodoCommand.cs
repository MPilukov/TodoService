using MediatR;

namespace Todo.Application.Todos.AddTodo
{
    /// <summary>
    /// Команда для добавления туду-записи
    /// </summary>
    public class AddTodoCommand : IRequest<AddTodoResult>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }    
}

