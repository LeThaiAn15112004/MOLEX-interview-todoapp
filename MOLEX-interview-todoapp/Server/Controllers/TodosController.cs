using Microsoft.AspNetCore.Mvc;
using MOLEX_interview_todoapp.Domain.Models;
using MOLEX_interview_todoapp.Domain.Services;
using MOLEX_interview_todoapp.Server.DTOs;

namespace MOLEX_interview_todoapp.Server.Controllers
{
    [ApiController]
    [Route("todos")] // doi thanh "api/[controller]" neu ban muon /api/todos
    public class TodosController : ControllerBase
    {
        private readonly ITodoServices _services;

        public TodosController(ITodoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Todo>>> GetAll()
        {
            var items = await _services.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Todo>> GetById(int id)
        {
            var todo = await _services.GetByIdAsync(id);
            if (todo is null)
            {
                return NotFound(new { message = "Todo not found." });
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create([FromBody] CreateTodoRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { message = "Title is required." });
            }

            var todo = new Todo { Title = request.Title.Trim() };

            var created = await _services.AddAsync(todo);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTodoRequest request)
        {
            if (request is null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (request.Completed is null)
            {
                return BadRequest(new { message = "Completed is required." });
            }

            if (request.Title is not null && string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { message = "Title cannot be empty." });
            }

            var existing = await _services.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { message = "Todo not found." });
            }

            if (request.Title is not null)
            {
                existing.Title = request.Title.Trim();
            }

            existing.Completed = request.Completed.Value;

            var updated = await _services.UpdateAsync(existing);
            if (!updated)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "Failed to update todo." }
                );
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _services.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Todo not found." });
            }

            return NoContent();
        }
    }
}
