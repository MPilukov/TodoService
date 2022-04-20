using MediatR;

namespace Todo.Application.Todos.UpdateTodo
{
    /// <summary>
    /// Команда для обновления туду-записи
    /// </summary>
    public class UpdateTodoCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }    
}

