using Todo.Application.Models;

namespace Todo.Application.Todos.GetTodo
{
    /// <summary>
    /// Результат получения туду-записи
    /// </summary>
    public class GetTodoResponse
    {
        public TodoItem Todo { get; set; }
    }
}