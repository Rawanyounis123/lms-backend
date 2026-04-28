using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Queries;

public record GetCourseByIdQuery(string Id) : IRequest<Course>;

public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Course>
{
    private readonly ICourseRepository _repo;

    public GetCourseByIdHandler(ICourseRepository repo) => _repo = repo;

    public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _repo.GetByIdAsync(request.Id);
        if (course is null) throw new KeyNotFoundException($"Course '{request.Id}' not found.");
        return course;
    }
}
