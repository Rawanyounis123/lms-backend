using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Features.Enrollments.Commands;
using LearningManagementSystem.Application.Features.Enrollments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers;

[ApiController]
[Route("api")]
public class EnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("students/{studentId}/enrollments")]
    public async Task<ActionResult<PagedResult<EnrollmentResponse>>> GetByStudent(
        string studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetEnrollmentsByStudentQuery(studentId, page, pageSize)));

    [HttpGet("courses/{courseId}/enrollments")]
    public async Task<ActionResult<PagedResult<EnrollmentResponse>>> GetByCourse(
        string courseId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetEnrollmentsByCourseQuery(courseId, page, pageSize)));

    [HttpPost("students/{studentId}/enrollments/{courseId}")]
    public async Task<ActionResult<EnrollmentResponse>> Enroll(string studentId, string courseId)
    {
        var created = await _mediator.Send(new EnrollStudentCommand(studentId, courseId));
        return CreatedAtAction(nameof(GetByStudent), new { studentId }, created);
    }

    [HttpDelete("students/{studentId}/enrollments/{courseId}")]
    public async Task<IActionResult> Unenroll(string studentId, string courseId)
    {
        await _mediator.Send(new UnenrollStudentCommand(studentId, courseId));
        return NoContent();
    }
}
