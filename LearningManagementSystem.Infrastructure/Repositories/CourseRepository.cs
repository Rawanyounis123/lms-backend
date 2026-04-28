using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Infrastructure.Data;
using MongoDB.Driver;

namespace LearningManagementSystem.Infrastructure.Repositories;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    private readonly IMongoCollection<Enrollment> _enrollments;

    public CourseRepository(IDbContext context) : base(context.Courses)
    {
        _enrollments = context.Enrollments;
    }

    public async Task<(List<Enrollment> Items, long TotalCount)> GetEnrollmentsByStudentIdAsync(string studentId, int page, int pageSize)
    {
        var filter = Builders<Enrollment>.Filter.Eq(e => e.StudentId, studentId);
        var totalCount = await _enrollments.CountDocumentsAsync(filter);
        var items = await _enrollments.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<(List<Enrollment> Items, long TotalCount)> GetEnrollmentsByCourseIdAsync(string courseId, int page, int pageSize)
    {
        var filter = Builders<Enrollment>.Filter.Eq(e => e.CourseId, courseId);
        var totalCount = await _enrollments.CountDocumentsAsync(filter);
        var items = await _enrollments.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<Enrollment?> GetEnrollmentAsync(string studentId, string courseId) =>
        await _enrollments
            .Find(e => e.StudentId == studentId && e.CourseId == courseId)
            .FirstOrDefaultAsync();

    public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
    {
        await _enrollments.InsertOneAsync(enrollment);
        return enrollment;
    }

    public async Task<bool> DeleteEnrollmentAsync(string studentId, string courseId)
    {
        var result = await _enrollments.DeleteOneAsync(
            e => e.StudentId == studentId && e.CourseId == courseId);
        return result.DeletedCount > 0;
    }
}
