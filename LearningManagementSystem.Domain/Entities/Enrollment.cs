using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearningManagementSystem.Domain.Entities;

public class Enrollment : BaseEntity
{
    [BsonElement("studentId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string StudentId { get; set; } = string.Empty;

    [BsonElement("courseId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CourseId { get; set; } = string.Empty;

    [BsonElement("enrolledAt")]
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}
