using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Exceptions;

namespace Todo.Application.Todos.GetTodo
{
    /// <summary>
    /// Обработчик для получения туду-записи
    /// </summary>
    public class GetTodoHandler : IRequestHandler<GetTodoQuery, GetTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<GetTodoResponse> Handle(GetTodoQuery query, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.FindTodoAsync(query.Id, cancellationToken);
            if (todo == null)
            {
                throw new NotFoundException($"Записи с ИД {query.Id} не найдено");
            }

            return new GetTodoResponse
            {
                Todo = todo,
            };
        }
    }
}