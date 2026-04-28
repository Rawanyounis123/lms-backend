using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearningManagementSystem.Infrastructure.Data;

public class MongoDbContext : IDbContext
{
    public IMongoCollection<Course> Courses { get; }
    public IMongoCollection<Student> Students { get; }
    public IMongoCollection<Enrollment> Enrollments { get; }

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        Courses = database.GetCollection<Course>("courses");
        Students = database.GetCollection<Student>("students");
        Enrollments = database.GetCollection<Enrollment>("enrollments");
    }
}
