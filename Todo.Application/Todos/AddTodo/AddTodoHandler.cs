using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Todo.Application.Models;

namespace Todo.Application.Todos.AddTodo
{
    /// <summary>
    /// Обработчик для добавления туду-записи
    /// </summary>
    public class AddTodoHandler : IRequestHandler<AddTodoCommand, AddTodoResult>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper; 

        public AddTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<AddTodoResult> Handle(AddTodoCommand command, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<TodoItem>(command);
            var id = await _todoRepository.AddTodoAsync(item, cancellationToken);

            return new AddTodoResult
            {
                Id = id,
            };
        }
    }
}