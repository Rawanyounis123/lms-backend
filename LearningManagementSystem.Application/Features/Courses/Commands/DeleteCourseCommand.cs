using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands;

public record DeleteCourseCommand(string Id) : IRequest;

public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly ICourseRepository _repo;

    public DeleteCourseHandler(ICourseRepository repo) => _repo = repo;

    public async Task Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        var deleted = await _repo.DeleteAsync(command.Id);
        if (!deleted) throw new KeyNotFoundException($"Course '{command.Id}' not found.");
    }
}
