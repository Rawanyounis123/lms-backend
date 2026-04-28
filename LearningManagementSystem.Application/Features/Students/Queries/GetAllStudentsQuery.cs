using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Students.Queries;

public record GetAllStudentsQuery(int Page, int PageSize) : IRequest<PagedResult<Student>>;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, PagedResult<Student>>
{
    private readonly IStudentRepository _repo;

    public GetAllStudentsHandler(IStudentRepository repo) => _repo = repo;

    public async Task<PagedResult<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<Student>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = items
        };
    }
}
