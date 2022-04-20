using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Todo.Application.Todos.GetAllTodos
{
    /// <summary>
    /// Обработчик для получения всех туду-записей
    /// </summary>
    public class GetAllTodosHandler : IRequestHandler<GetAllTodosQuery, GetAllTodosResponse>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<GetAllTodosResponse> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
        {
            var response = await _todoRepository.GetAllTodosAsync(cancellationToken);
            return new GetAllTodosResponse
            {
                Todos = response,
            };
        }
    }
}