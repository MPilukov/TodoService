using MediatR;

namespace Todo.Application.Todos.GetAllTodos
{
    /// <summary>
    /// Запрос для получения всех туду-записей
    /// </summary>
    public class GetAllTodosQuery : IRequest<GetAllTodosResponse>
    {
    }
}