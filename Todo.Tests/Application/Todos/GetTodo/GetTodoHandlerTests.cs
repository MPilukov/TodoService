using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Todo.Application.Exceptions;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Application.Todos.GetTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.GetTodo
{
    /// <summary>
    /// Тесты для <see cref="GetTodoHandler"/>
    /// </summary>
    public class GetTodoHandlerTests : BaseTest
    {
        /// <summary>
        /// Handler_TodoNotFound_ThrowsAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoNotFound_ThrowsAsync()
        {
            // Arrange
            var query = Fixture.Create<GetTodoQuery>();

            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            query.Id,
                            It.IsAny<CancellationToken>()))!
                .ReturnsAsync((TodoItem)null);

            var handler = Mock.Create<GetTodoHandler>();

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, default));

            // Assert
            Assert.Equal($"Записи с ИД {query.Id} не найдено", ex.Message);
        }

        /// <summary>
        /// Handler_TodoIsExist_ReturnExistTodoFromRepositoryAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoIsExist_ReturnExistTodoFromRepositoryAsync()
        {
            // Arrange
            var query = Fixture.Create<GetTodoQuery>();
            var todo = Fixture.Create<TodoItem>();

            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            query.Id,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(todo);

            var handler = Mock.Create<GetTodoHandler>();

            // Act
            var response = await handler.Handle(query, default);

            // Assert
            Assert.Equal(response.Todo, todo);
        }
    }
}