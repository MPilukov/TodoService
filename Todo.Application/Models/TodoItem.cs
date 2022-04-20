namespace Todo.Application.Models
{
    /// <summary>
    /// Туду-запись
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Ид записи
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Название записи
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Выполнена ли тудушка
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
