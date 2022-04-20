using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Todo.Application.Exceptions;
using Todo.Application.Models;

namespace Todo.Application.Todos.UpdateTodo
{
    /// <summary>
    /// Обработчик для обновления туду-записи
    /// </summary>
    public class UpdateTodoHandler : AsyncRequestHandler<UpdateTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper; 

        public UpdateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
        {
            var existTodo = await _todoRepository.FindTodoAsync(command.Id, cancellationToken);
            if (existTodo == null)
            {
                throw new NotFoundException($"Записи с ИД {command.Id} не найдено. Обновление невозможно");
            }

            var todo = _mapper.Map<TodoItem>(command);
            await _todoRepository.UpdateTodoAsync(todo, cancellationToken);
        }
    }
}