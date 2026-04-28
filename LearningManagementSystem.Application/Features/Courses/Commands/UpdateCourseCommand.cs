using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands;

public record UpdateCourseCommand(string Id, UpdateCourseRequest Request) : IRequest;

public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly ICourseRepository _repo;

    public UpdateCourseHandler(ICourseRepository repo) => _repo = repo;

    public async Task Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Course '{command.Id}' not found.");

        existing.Title = command.Request.Title;
        existing.Description = command.Request.Description;
        existing.InstructorName = command.Request.InstructorName;
        existing.Credits = command.Request.Credits;

        await _repo.UpdateAsync(command.Id, existing);
    }
}
