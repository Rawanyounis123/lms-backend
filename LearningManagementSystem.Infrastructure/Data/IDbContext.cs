using LearningManagementSystem.Domain.Entities;
using MongoDB.Driver;

namespace LearningManagementSystem.Infrastructure.Data;

public interface IDbContext
{
    IMongoCollection<Course> Courses { get; }
    IMongoCollection<Student> Students { get; }
    IMongoCollection<Enrollment> Enrollments { get; }
}
