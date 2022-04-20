using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Todo.Application.Todos.AddTodo;
using Todo.Application.Todos.DeleteTodo;
using Todo.Application.Todos.GetAllTodos;
using Todo.Application.Todos.GetTodo;
using Todo.Application.Todos.UpdateTodo;

namespace Todo.Api.Controllers
{
    /// <summary>
    /// Контролле для работы с туду-записями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Метод получения всех туду-записей
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="GetAllTodosResponse"/></returns>
        [HttpGet]
        [SwaggerOperation(
            OperationId = nameof(GetTodoItems),
            Summary = "Метод получения всех туду-записей")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetAllTodosResponse))]
        public async Task<ActionResult<GetAllTodosResponse>> GetTodoItems(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllTodosQuery(), cancellationToken);
        }

        /// <summary>
        /// Метод получения туду-записи
        /// </summary>
        /// <param name="id">ИД записи</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="GetTodoResponse"/></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            OperationId = nameof(GetTodoItem),
            Summary = "Метод получения туду-записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetTodoResponse))]
        public async Task<ActionResult<GetTodoResponse>> GetTodoItem(long id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTodoQuery
            {
                Id = id,
            }, cancellationToken);
        }

        /// <summary>
        /// Метод обновления туду-записи
        /// </summary>
        /// <param name="command"><see cref="UpdateTodoCommand"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPut]
        [SwaggerOperation(
            OperationId = nameof(UpdateTodoItem),
            Summary = "Метод обновления туду-записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> UpdateTodoItem(UpdateTodoCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Метод добавления туду-записи
        /// </summary>
        /// <param name="command"><see cref="AddTodoCommand"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="AddTodoResult"/></returns>
        [HttpPost]
        [SwaggerOperation(
            OperationId = nameof(CreateTodoItem),
            Summary = "Метод добавления туду-записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        public async Task<ActionResult<AddTodoResult>> CreateTodoItem(AddTodoCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Метод удаления туду-записи
        /// </summary>
        /// <param name="id">ИД записи</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            OperationId = nameof(DeleteTodoItem),
            Summary = "Метод удаления туду-записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTodoCommand
            {
                Id = id,
            }, cancellationToken);
            return NoContent();
        }
    }
}
