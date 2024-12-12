using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentToDoApi.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Username { get; set; }

        [BsonRequired]
        public string PasswordHash { get; set; } // Password is hashed for security
    }
}
