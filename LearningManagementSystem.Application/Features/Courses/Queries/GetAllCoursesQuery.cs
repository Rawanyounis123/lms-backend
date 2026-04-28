using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Queries;

public record GetAllCoursesQuery(int Page, int PageSize) : IRequest<PagedResult<Course>>;

public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, PagedResult<Course>>
{
    private readonly ICourseRepository _repo;

    public GetAllCoursesHandler(ICourseRepository repo) => _repo = repo;

    public async Task<PagedResult<Course>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<Course>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = items
        };
    }
}
