using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Students.Commands;

public record UpdateStudentCommand(string Id, UpdateStudentRequest Request) : IRequest;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _repo;

    public UpdateStudentHandler(IStudentRepository repo) => _repo = repo;

    public async Task Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Student '{command.Id}' not found.");

        existing.FullName = command.Request.FullName;
        existing.Email = command.Request.Email;
        existing.DateOfBirth = command.Request.DateOfBirth;

        await _repo.UpdateAsync(command.Id, existing);
    }
}
