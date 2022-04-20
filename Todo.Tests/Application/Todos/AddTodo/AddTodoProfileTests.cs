using AutoFixture;
using Todo.Application.Models;
using Todo.Application.Todos.AddTodo;
using Xunit;

namespace Todo.Tests.Application.Todos.AddTodo
{
    /// <summary>
    /// Тесты для <see cref="AddTodoProfile"/>
    /// </summary>
    public class AddTodoProfileTests : BaseTest
    {
        /// <summary>
        /// Mapper_MapAddTodoCommandToTodoItem_FieldEquals
        /// </summary>
        [Fact]
        public void Mapper_MapAddTodoCommandToTodoItem_FieldEquals()
        {
            // Arrange
            var command = Fixture.Create<AddTodoCommand>();

            // Act
            var response = Mapper.Map<TodoItem>(command);

            // Assert
            Assert.Equal(response.Name, command.Name);
            Assert.Equal(response.IsComplete, command.IsComplete);
        }
    }
}