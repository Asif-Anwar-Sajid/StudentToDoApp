using Microsoft.AspNetCore.Mvc;
using StudentToDoApi.Models;
using StudentToDoApi.Services;

namespace StudentToDoApi.Controllers
{
    [ApiController] // Indicates that this class is an API controller.
    [Route("api/[controller]")] // Automatically maps to '/api/ToDo'.
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDo toDo)
        {
            // Create a new task using the service
            var newTask = await _toDoService.CreateTaskAsync(toDo.StudentId, toDo.Task);

            // Return a 201 Created response with a link to the new task
            return CreatedAtAction(nameof(CreateTask), new { id = newTask.Id }, newTask);
        }

        // GET: api/ToDo/{studentId}
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetTasks(string studentId)
        {
            var tasks = await _toDoService.GetTasksAsync(studentId);
            return Ok(tasks);
        }

        // PUT: api/ToDo/{taskId}
        [HttpPut("{taskId}")]
        public async Task<IActionResult> MarkTaskCompleted(string taskId)
        {
            var success = await _toDoService.MarkTaskCompletedAsync(taskId);
            if (success)
                return Ok(new { message = "Task marked as completed." });

            return NotFound(new { message = "Task not found." });
        }

        // DELETE: api/ToDo/{taskId}
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            var success = await _toDoService.DeleteTaskAsync(taskId);
            if (success)
                return Ok(new { message = "Task deleted successfully." });

            return NotFound(new { message = "Task not found." });
        }
    }
}
