using MongoDB.Driver;
using StudentToDoApi.Models;

namespace StudentToDoApi.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        
        // Constructor to initialize the MongoDB connection
        public MongoContext(string connectionString, string databaseName)
        {
            // Create a MongoClient using the provided connection sting
            var client = new MongoClient(connectionString);

            // Access the specific database within MongoDB
            _database = client.GetDatabase(databaseName);
        }

        // Provide access to the "Students" collection
        public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students"); // fetches the Students collection from MongoDB

        // Provide access to the "ToDos" collection
        public IMongoCollection<ToDo> ToDos => _database.GetCollection<ToDo>("ToDos"); // fetches the ToDoS collection from MongoDB
    }
}
