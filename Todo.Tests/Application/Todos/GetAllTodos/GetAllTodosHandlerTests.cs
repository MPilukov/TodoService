using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Application.Todos.GetAllTodos;
using Xunit;

namespace Todo.Tests.Application.Todos.GetAllTodos
{
    /// <summary>
    /// Тесты для <see cref="GetAllTodosHandler"/>
    /// </summary>
    public class GetAllTodosHandlerTests : BaseTest
    {
        /// <summary>
        /// Handler_PositiveCase_ReturnExistTodosFromRepositoryAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_PositiveCase_ReturnExistTodosFromRepositoryAsync()
        {
            // Arrange
            var query = Fixture.Create<GetAllTodosQuery>();
            var todos = Fixture.CreateMany<TodoItem>().ToList();

            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.GetAllTodosAsync(
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(todos);

            var handler = Mock.Create<GetAllTodosHandler>();

            // Act
            var response = await handler.Handle(query, default);

            // Assert
            Assert.Equal(response.Todos, todos);
        }
    }
}