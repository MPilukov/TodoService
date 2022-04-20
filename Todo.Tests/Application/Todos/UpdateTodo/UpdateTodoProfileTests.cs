using AutoFixture;
using Todo.Application.Models;
using Todo.Application.Todos.UpdateTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.UpdateTodo
{
    /// <summary>
    /// Тесты для <see cref="UpdateTodoProfile"/>
    /// </summary>
    public class UpdateTodoProfileTests : BaseTest
    {
        /// <summary>
        /// Mapper_MapAddTodoCommandToTodoItem_FieldEquals
        /// </summary>
        [Fact]
        public void Mapper_MapAddTodoCommandToTodoItem_FieldEquals()
        {
            // Arrange
            var command = Fixture.Create<UpdateTodoCommand>();

            // Act
            var response = Mapper.Map<TodoItem>(command);

            // Assert
            Assert.Equal(response.Id, command.Id);
            Assert.Equal(response.Name, command.Name);
            Assert.Equal(response.IsComplete, command.IsComplete);
        }
    }
}