using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Students.Commands;

public record DeleteStudentCommand(string Id) : IRequest;

public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _repo;

    public DeleteStudentHandler(IStudentRepository repo) => _repo = repo;

    public async Task Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
    {
        var deleted = await _repo.DeleteAsync(command.Id);
        if (!deleted) throw new KeyNotFoundException($"Student '{command.Id}' not found.");
    }
}
