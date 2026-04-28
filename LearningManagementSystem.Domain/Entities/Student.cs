using MongoDB.Bson.Serialization.Attributes;

namespace LearningManagementSystem.Domain.Entities;

public class Student : BaseEntity
{
    [BsonElement("fullName")]
    public string FullName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }
}
