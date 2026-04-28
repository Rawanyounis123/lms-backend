using LearningManagementSystem.Domain.Entities;
using MongoDB.Driver;

namespace LearningManagementSystem.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IDbContext db)
    {
        await CreateIndexesAsync(db);
        await SeedAsync(db);
    }

    private static async Task CreateIndexesAsync(IDbContext db)
    {
        var studentEmailIndex = new CreateIndexModel<Student>(
            Builders<Student>.IndexKeys.Ascending(s => s.Email),
            new CreateIndexOptions { Unique = true, Name = "unique_student_email" });

        await db.Students.Indexes.CreateOneAsync(studentEmailIndex);

        var enrollmentUniqueIndex = new CreateIndexModel<Enrollment>(
            Builders<Enrollment>.IndexKeys
                .Ascending(e => e.StudentId)
                .Ascending(e => e.CourseId),
            new CreateIndexOptions { Unique = true, Name = "unique_enrollment" });

        var enrollmentByCourseIndex = new CreateIndexModel<Enrollment>(
            Builders<Enrollment>.IndexKeys.Ascending(e => e.CourseId),
            new CreateIndexOptions { Name = "idx_enrollment_courseId" });

        await db.Enrollments.Indexes.CreateManyAsync(
            [enrollmentUniqueIndex, enrollmentByCourseIndex]);
    }

    private static async Task SeedAsync(IDbContext db)
    {
        var anyCourse = await db.Courses.Find(_ => true).AnyAsync();
        if (anyCourse) return;

        var courses = new List<Course>
        {
            new() { Title = "Introduction to C#",        Description = "C# fundamentals for beginners.",           InstructorName = "Dr. Alice Carter",   Credits = 3 },
            new() { Title = "Web Development with ASP.NET", Description = "Build REST APIs with ASP.NET Core.",    InstructorName = "Prof. Bob Miller",   Credits = 4 },
            new() { Title = "Database Design",            Description = "Relational and NoSQL database concepts.", InstructorName = "Dr. Carol White",    Credits = 3 },
        };

        await db.Courses.InsertManyAsync(courses);

        var anyStudent = await db.Students.Find(_ => true).AnyAsync();
        if (anyStudent) return;

        var students = new List<Student>
        {
            new() { FullName = "Alice Johnson", Email = "alice@example.com",  DateOfBirth = new DateTime(2000, 5, 15, 0, 0, 0, DateTimeKind.Utc) },
            new() { FullName = "Bob Smith",     Email = "bob@example.com",    DateOfBirth = new DateTime(1999, 8, 22, 0, 0, 0, DateTimeKind.Utc) },
            new() { FullName = "Carol Davis",   Email = "carol@example.com",  DateOfBirth = new DateTime(2001, 3, 10, 0, 0, 0, DateTimeKind.Utc) },
        };

        await db.Students.InsertManyAsync(students);

        var enrollments = new List<Enrollment>
        {
            new() { StudentId = students[0].Id, CourseId = courses[0].Id },
            new() { StudentId = students[0].Id, CourseId = courses[1].Id },
            new() { StudentId = students[1].Id, CourseId = courses[0].Id },
            new() { StudentId = students[2].Id, CourseId = courses[2].Id },
        };

        await db.Enrollments.InsertManyAsync(enrollments);
    }
}
