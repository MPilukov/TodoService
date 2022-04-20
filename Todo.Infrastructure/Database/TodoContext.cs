using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Infrastructure.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemEntity> TodoItems { get; set; }
    }
}