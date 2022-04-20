using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Exceptions;

namespace Todo.Application.Todos.DeleteTodo
{
    /// <summary>
    /// Обработчик для удаления туду-записи
    /// </summary>
    public class DeleteTodoHandler : AsyncRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        protected override async Task Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
        {
            var existTodo = await _todoRepository.FindTodoAsync(command.Id, cancellationToken);
            if (existTodo == null)
            {
                throw new NotFoundException($"Записи с ИД {command.Id} не найдено. Удаление невозможно");
            }

            await _todoRepository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}