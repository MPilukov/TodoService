using System.Collections.Generic;
using Todo.Application.Models;

namespace Todo.Application.Todos.GetAllTodos
{
    /// <summary>
    /// Результат получения всех туду-записей
    /// </summary>
    public class GetAllTodosResponse
    {
        /// <summary>
        /// Список туду-записей
        /// </summary>
        public IEnumerable<TodoItem> Todos { get; set; }
    }
}