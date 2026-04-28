using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Students.Commands;

public record CreateStudentCommand(CreateStudentRequest Request) : IRequest<Student>;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Student>
{
    private readonly IStudentRepository _repo;

    public CreateStudentHandler(IStudentRepository repo) => _repo = repo;

    public Task<Student> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FullName = command.Request.FullName,
            Email = command.Request.Email,
            DateOfBirth = command.Request.DateOfBirth
        };
        return _repo.CreateAsync(student);
    }
}
