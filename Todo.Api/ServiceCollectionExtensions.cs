using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Todos;
using Todo.Infrastructure.Database.Repositories;

namespace Todo.Api
{
    public static class ServiceCollectionExtensions
    {
        private static string ApplicationProjectNameSpace => $"Todo.Application";
        private static string InfrastructureProjectNameSpace => $"Todo.Infrastructure";

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.Scan(scan =>
            {
                scan
                    .FromAssemblies(
                        Assembly.Load(ApplicationProjectNameSpace),
                        Assembly.Load(InfrastructureProjectNameSpace))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestHandler<>)))
                    .AsImplementedInterfaces()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.AllowNullDestinationValues = true;
                },
                Assembly.Load(ApplicationProjectNameSpace),
                Assembly.Load(InfrastructureProjectNameSpace));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}