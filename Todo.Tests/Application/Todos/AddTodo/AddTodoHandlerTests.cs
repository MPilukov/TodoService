using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Moq;
using Todo.Application.Models;
using Todo.Application.Todos;
using Todo.Application.Todos.AddTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.AddTodo
{
    /// <summary>
    /// Тесты для <see cref="AddTodoHandler"/>
    /// </summary>
    public class AddTodoHandlerTests : BaseTest
    {
        /// <summary>
        /// Handler_PositiveCase_ReturnCreatedIdFromRepositoryAsync
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        [Fact]
        public async Task Handler_PositiveCase_ReturnCreatedIdFromRepositoryAsync()
        {
            // Arrange
            var command = Fixture.Create<AddTodoCommand>();
            var createdId = Fixture.Create<long>();
            var todo = Fixture.Create<TodoItem>();

            Mock.Mock<IMapper>().Setup(
                    x =>
                        x.Map<TodoItem>(
                            It.Is<AddTodoCommand>(c => c == command)))
                .Returns(todo);
            Mock.Mock<ITodoRepository>().Setup(
                    x =>
                        x.AddTodoAsync(
                            It.IsAny<TodoItem>(),
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdId);

            var handler = Mock.Create<AddTodoHandler>();

            // Act
            var response = await handler.Handle(command, default);

            // Assert
            Assert.Equal(response.Id, createdId);
        }
    }
}