
using Microsoft.AspNetCore.Mvc;
using StudentToDoApi.Models;
using StudentToDoApi.Services;

namespace StudentToDoApi.Controllers
{
    [ApiController] // Indicates that this class handles API requests.
    [Route("api/[controller]")] // Automatically maps the route to '/api/Student'
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        // POST: api/Student/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Student student)
        {
            try
            {
                var newStudent = await _studentService.RegisterAsync(student.Username, student.PasswordHash);
                return CreatedAtAction(nameof(Register), new { id = newStudent.Id }, newStudent);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // POST: api/Student/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Student student)
        {
            try
            {
                var loggedInStudent = await _studentService.LoginAsync(student.Username, student.PasswordHash);
                return Ok(loggedInStudent);
            }
            catch (Exception ex)
            {
                return Unauthorized(new {message = ex.Message});
            }
        }
    }
}
