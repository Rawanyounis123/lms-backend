using MongoDB.Bson.Serialization.Attributes;

namespace LearningManagementSystem.Domain.Entities;

public class Course : BaseEntity
{
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("instructorName")]
    public string InstructorName { get; set; } = string.Empty;

    [BsonElement("credits")]
    public int Credits { get; set; }
}
