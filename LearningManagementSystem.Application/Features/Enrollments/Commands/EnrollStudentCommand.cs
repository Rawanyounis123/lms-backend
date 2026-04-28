using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands;

public record EnrollStudentCommand(string StudentId, string CourseId) : IRequest<EnrollmentResponse>;

public class EnrollStudentHandler : IRequestHandler<EnrollStudentCommand, EnrollmentResponse>
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public EnrollStudentHandler(IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public async Task<EnrollmentResponse> Handle(EnrollStudentCommand command, CancellationToken cancellationToken)
    {
        var student = await _studentRepo.GetByIdAsync(command.StudentId)
            ?? throw new KeyNotFoundException($"Student '{command.StudentId}' not found.");

        var course = await _courseRepo.GetByIdAsync(command.CourseId)
            ?? throw new KeyNotFoundException($"Course '{command.CourseId}' not found.");

        var existing = await _courseRepo.GetEnrollmentAsync(command.StudentId, command.CourseId);
        if (existing is not null)
            throw new InvalidOperationException("Student is already enrolled in this course.");

        var enrollment = new Enrollment
        {
            StudentId = command.StudentId,
            CourseId = command.CourseId,
            EnrolledAt = DateTime.UtcNow
        };
        var created = await _courseRepo.CreateEnrollmentAsync(enrollment);

        return new EnrollmentResponse
        {
            Id = created.Id,
            EnrolledAt = created.EnrolledAt,
            Student = new StudentSummary { Id = student.Id, Name = student.FullName }
        };
    }
}
