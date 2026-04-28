using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands;

public record CreateCourseCommand(CreateCourseRequest Request) : IRequest<Course>;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Course>
{
    private readonly ICourseRepository _repo;

    public CreateCourseHandler(ICourseRepository repo) => _repo = repo;

    public Task<Course> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Title = command.Request.Title,
            Description = command.Request.Description,
            InstructorName = command.Request.InstructorName,
            Credits = command.Request.Credits
        };
        return _repo.CreateAsync(course);
    }
}
