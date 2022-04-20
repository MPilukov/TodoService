using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Models;

namespace Todo.Application.Todos
{
    /// <summary>
    /// Репозиторий для работы с туду-записями
    /// </summary>
    public interface ITodoRepository
    {
        /// <summary>
        /// Добавить <see cref="TodoItem"/>
        /// </summary>
        /// <param name="todoItem"><see cref="TodoItem"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>ИД записи</returns>
        Task<long> AddTodoAsync(TodoItem todoItem, CancellationToken cancellationToken);
        
        /// <summary>
        /// Обновить <see cref="TodoItem"/>
        /// </summary>
        /// <param name="todoItem"><see cref="TodoItem"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken);
        
        /// <summary>
        /// Найти <see cref="TodoItem"/> по ИД
        /// </summary>
        /// <param name="id">ИД записи</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="TodoItem"/></returns>
        Task<TodoItem> FindTodoAsync(long id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить все <see cref="TodoItem"/>
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Список <see cref="TodoItem"/></returns>
        Task<IEnumerable<TodoItem>> GetAllTodosAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// Удалить <see cref="TodoItem"/> 
        /// </summary>
        /// <param name="id">ИД записи</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}