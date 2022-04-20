using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using Todo.Application.Exceptions;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Application.Todos.DeleteTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.DeleteTodo
{
    /// <summary>
    /// Тесты для <see cref="DeleteTodoHandler"/>
    /// </summary>
    public class DeleteTodoHandlerTests : BaseTest
    {
        /// <summary>
        /// Handler_TodoNotFound_ThrowsAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoNotFound_ThrowsAsync()
        {
            // Arrange
            var command = Fixture.Create<DeleteTodoCommand>();

            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            command.Id,
                            It.IsAny<CancellationToken>()))!
                .ReturnsAsync((TodoItem)null);

            var handler = Mock.Create<DeleteTodoHandler>() as IRequestHandler<DeleteTodoCommand>;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));

            // Assert
            Assert.Equal($"Записи с ИД {command.Id} не найдено. Удаление невозможно", ex.Message);
        }
        
        /// <summary>
        /// Handler_TodoExist_DeleteTodoInRepositoryAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoExist_DeleteTodoInRepositoryAsync()
        {
            // Arrange
            var command = Fixture.Create<DeleteTodoCommand>();
            var todo = Fixture.Create<TodoItem>();
            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            command.Id,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(todo);

            var handler = Mock.Create<DeleteTodoHandler>() as IRequestHandler<DeleteTodoCommand>;

            // Act
            await handler.Handle(command, default);

            // Assert
            Mock.Mock<ITodoRepository>()
                .Verify(
                    x =>
                        x.DeleteAsync(
                            command.Id,
                            It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}