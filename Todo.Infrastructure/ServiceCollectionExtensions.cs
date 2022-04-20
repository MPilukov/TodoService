using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Database;

namespace Todo.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        // create an run migrations 
        // cd TodoService
        // dotnet ef migrations add InitialCreate --project Todo.Infrastructure --startup-project Todo.Api
        // dotnet ef database update InitialCreate --project Todo.Infrastructure --startup-project Todo.Api
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            string connectionString) => services
            .AddDbContext<TodoContext>(
                o => o.UseSqlServer(connectionString),
                ServiceLifetime.Transient);
    }
}