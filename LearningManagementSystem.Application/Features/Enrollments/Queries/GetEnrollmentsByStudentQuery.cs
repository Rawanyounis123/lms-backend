using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Interfaces;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries;

public record GetEnrollmentsByStudentQuery(string StudentId, int Page, int PageSize) : IRequest<PagedResult<EnrollmentResponse>>;

public class GetEnrollmentsByStudentHandler : IRequestHandler<GetEnrollmentsByStudentQuery, PagedResult<EnrollmentResponse>>
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public GetEnrollmentsByStudentHandler(IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public async Task<PagedResult<EnrollmentResponse>> Handle(GetEnrollmentsByStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepo.GetByIdAsync(request.StudentId)
            ?? throw new KeyNotFoundException($"Student '{request.StudentId}' not found.");

        var (enrollments, totalCount) = await _courseRepo.GetEnrollmentsByStudentIdAsync(request.StudentId, request.Page, request.PageSize);

        var studentSummary = new StudentSummary { Id = student.Id, Name = student.FullName };

        var items = enrollments.Select(e => new EnrollmentResponse
        {
            Id = e.Id,
            EnrolledAt = e.EnrolledAt,
            Student = studentSummary
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
