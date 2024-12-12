using MongoDB.Driver;
using StudentToDoApi.Data;
using StudentToDoApi.Models;

namespace StudentToDoApi.Services
{
    public class ToDoService
    {
        private readonly IMongoCollection<ToDo> _toDos;

        // Constructor: Inject MongoContext to access the database
        public ToDoService(MongoContext context)
        {
            _toDos = context.ToDos;
        }

        // Create a new task
        public async Task<ToDo> CreateTaskAsync(string studentId, string task)
        {
            var newTask = new ToDo
            {
                StudentId = studentId,
                Task = task
            };
            
            await _toDos.InsertOneAsync(newTask);
            return newTask;
        }

        // Get all tasks for a student 
        public async Task<List<ToDo>> GetTasksAsync(string studentId)
        {
            return await _toDos.Find(t => t.StudentId == studentId).ToListAsync();
        }

        // Mark a task as completed
        public async Task<bool> MarkTaskCompletedAsync(string TaskId)
        {
            var result = await _toDos.UpdateOneAsync(
                t => t.Task == TaskId,
                Builders<ToDo>.Update.Set(t => t.IsCompleted, true)
            );

            return result.ModifiedCount > 0;
        }

        // Delete a task
        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var result = await _toDos.DeleteOneAsync(t => t.Id == taskId);
            return result.DeletedCount > 0;
        }
    }
}
