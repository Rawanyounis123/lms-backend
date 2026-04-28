using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands;

public record UnenrollStudentCommand(string StudentId, string CourseId) : IRequest;

public class UnenrollStudentHandler : IRequestHandler<UnenrollStudentCommand>
{
    private readonly ICourseRepository _courseRepo;

    public UnenrollStudentHandler(ICourseRepository courseRepo) => _courseRepo = courseRepo;

    public async Task Handle(UnenrollStudentCommand command, CancellationToken cancellationToken)
    {
        var deleted = await _courseRepo.DeleteEnrollmentAsync(command.StudentId, command.CourseId);
        if (!deleted) throw new KeyNotFoundException("Enrollment not found.");
    }
}
