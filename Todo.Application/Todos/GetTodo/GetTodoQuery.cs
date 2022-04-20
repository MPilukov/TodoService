using MediatR;

namespace Todo.Application.Todos.GetTodo
{
    /// <summary>
    /// Запрос для получения туду-записи
    /// </summary>
    public class GetTodoQuery : IRequest<GetTodoResponse>
    {
        public long Id { get; set; }
    }
}