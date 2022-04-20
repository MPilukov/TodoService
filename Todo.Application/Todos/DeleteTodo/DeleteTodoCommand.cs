using MediatR;

namespace Todo.Application.Todos.DeleteTodo
{
    /// <summary>
    /// Команда для удаления туду-записи
    /// </summary>
    public class DeleteTodoCommand : IRequest
    {
        public long Id { get; set; }
    }
}