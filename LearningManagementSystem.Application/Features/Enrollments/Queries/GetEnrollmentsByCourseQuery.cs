using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries;

public record GetEnrollmentsByCourseQuery(string CourseId, int Page, int PageSize) : IRequest<PagedResult<EnrollmentResponse>>;

public class GetEnrollmentsByCourseHandler : IRequestHandler<GetEnrollmentsByCourseQuery, PagedResult<EnrollmentResponse>>
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public GetEnrollmentsByCourseHandler(IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public async Task<PagedResult<EnrollmentResponse>> Handle(GetEnrollmentsByCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepo.GetByIdAsync(request.CourseId)
            ?? throw new KeyNotFoundException($"Course '{request.CourseId}' not found.");

        var (enrollments, totalCount) = await _courseRepo.GetEnrollmentsByCourseIdAsync(request.CourseId, request.Page, request.PageSize);

        var studentIds = enrollments.Select(e => e.StudentId).Distinct();
        var students = await _studentRepo.GetByIdsAsync(studentIds);
        var studentMap = students.ToDictionary(s => s.Id);

        var items = enrollments.Select(e => new EnrollmentResponse
        {
            Id = e.Id,
            EnrolledAt = e.EnrolledAt,
            Student = studentMap.TryGetValue(e.StudentId, out var s)
                ? new StudentSummary { Id = s.Id, Name = s.FullName }
                : new StudentSummary { Id = e.StudentId, Name = string.Empty }
        }).ToList();

        return new PagedResult<EnrollmentResponse>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = items
        };
    }
}
