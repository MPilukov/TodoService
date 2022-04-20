using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using MediatR;
using Moq;
using Todo.Application.Exceptions;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Application.Todos.UpdateTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.UpdateTodo
{
    /// <summary>
    /// Тесты для <see cref="UpdateTodoHandler"/>
    /// </summary>
    public class UpdateTodoHandlerTests : BaseTest
    {
        /// <summary>
        /// Handler_TodoNotFound_ThrowsAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoNotFound_ThrowsAsync()
        {
            // Arrange
            var command = Fixture.Create<UpdateTodoCommand>();

            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            command.Id,
                            It.IsAny<CancellationToken>()))!
                .ReturnsAsync((TodoItem)null);

            var handler = Mock.Create<UpdateTodoHandler>() as IRequestHandler<UpdateTodoCommand>;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));

            // Assert
            Assert.Equal($"Записи с ИД {command.Id} не найдено. Обновление невозможно", ex.Message);
        }

        /// <summary>
        /// Handler_TodoExist_UpdateTodoInRepositoryAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_TodoExist_UpdateTodoInRepositoryAsync()
        {
            // Arrange
            var command = Fixture.Create<UpdateTodoCommand>();
            var oldTodo = Fixture.Create<TodoItem>();
            var todo = Fixture.Create<TodoItem>();
            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.FindTodoAsync(
                            command.Id,
                            It.IsAny<CancellationToken>()))!
                .ReturnsAsync(oldTodo);
            Mock.Mock<IMapper>().Setup(
                    x =>
                        x.Map<TodoItem>(
                            It.Is<UpdateTodoCommand>(c => c == command)))
                .Returns(todo);

            var handler = Mock.Create<UpdateTodoHandler>() as IRequestHandler<UpdateTodoCommand>;

            // Act
            await handler.Handle(command, default);

            // Assert
            Mock.Mock<ITodoRepository>()
                .Verify(
                    x =>
                        x.UpdateTodoAsync(
                            todo,
                            It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}