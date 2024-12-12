using StudentToDoApi.Models;
using MongoDB.Driver;
using StudentToDoApi.Data;
using BCrypt.Net;
using System.Threading.Tasks;

namespace StudentToDoApi.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        // Constructor: Inject MongoContext to access the database
        public StudentService(MongoContext context)
        {   
            _students = context.Students;
        }

        // Method to register a new student
        public async Task<Student> RegisterAsync(string username, string password)
        {
            var existingStudent = await _students.Find(s => s.Username == username).FirstOrDefaultAsync();
            if(existingStudent != null)
            {
                throw new Exception("Student with the username already exits.");
            }

            // Hash the password for security
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Create a new Student object
            var newStudent = new Student
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            // Insert the new student into the database
            await _students.InsertOneAsync(newStudent);


            return newStudent;
        }

        // Method to authenticate a student
        public async Task<Student> LoginAsync(string username, string password)
        {
            // Find the student by username
            var student = await _students.Find(s => s.Username == username).FirstOrDefaultAsync();

            // Validate the username and password
            if (student == null || !BCrypt.Net.BCrypt.Verify(password, student.PasswordHash))
                throw new Exception("Invalid username or password.");

            return student;
        }
    }
}
