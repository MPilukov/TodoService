using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Exceptions;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Infrastructure.Database.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> AddTodoAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TodoItemEntity>(todoItem);
            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            var entity = await FindEntityById(todoItem.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Не найдена запись с ИД {todoItem.Id} для обновления");
            }

            entity.Name = todoItem.Name;
            entity.IsComplete = todoItem.IsComplete;
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TodoItem> FindTodoAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await FindEntityById(id, cancellationToken);
            return entity == null ? null : _mapper.Map<TodoItem>(entity);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync(CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .AsNoTracking()
                .Select(x => _mapper.Map<TodoItem>(x))
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await FindEntityById(id, cancellationToken);
            
            if (entity == null)
            {
                throw new NotFoundException($"Не найдена запись с ИД {id} для удаления");
            }
            
            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private Task<TodoItemEntity> FindEntityById(long id, CancellationToken cancellationToken)
        {
            return _context.TodoItems
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}