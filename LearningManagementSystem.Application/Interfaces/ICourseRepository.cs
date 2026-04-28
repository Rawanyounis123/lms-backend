using LearningManagementSystem.Domain.Entities;

namespace LearningManagementSystem.Application.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<(List<Enrollment> Items, long TotalCount)> GetEnrollmentsByStudentIdAsync(string studentId, int page, int pageSize);
    Task<(List<Enrollment> Items, long TotalCount)> GetEnrollmentsByCourseIdAsync(string courseId, int page, int pageSize);
    Task<Enrollment?> GetEnrollmentAsync(string studentId, string courseId);
    Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);
    Task<bool> DeleteEnrollmentAsync(string studentId, string courseId);
}
