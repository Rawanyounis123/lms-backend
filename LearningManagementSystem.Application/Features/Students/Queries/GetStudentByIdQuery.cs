using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Students.Queries;

public record GetStudentByIdQuery(string Id) : IRequest<Student>;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Student>
{
    private readonly IStudentRepository _repo;

    public GetStudentByIdHandler(IStudentRepository repo) => _repo = repo;

    public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _repo.GetByIdAsync(request.Id);
        if (student is null) throw new KeyNotFoundException($"Student '{request.Id}' not found.");
        return student;
    }
}
