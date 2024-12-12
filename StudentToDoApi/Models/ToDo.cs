using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentToDoApi.Models
{
    public class ToDo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string StudentId { get; set; } // Links to the student
        public string Task {  get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
