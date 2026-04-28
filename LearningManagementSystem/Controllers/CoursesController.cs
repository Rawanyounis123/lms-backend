using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Features.Courses.Commands;
using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<PagedResult<Course>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetAllCoursesQuery(page, pageSize)));

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetById(string id) =>
        Ok(await _mediator.Send(new GetCourseByIdQuery(id)));

    [HttpPost]
    public async Task<ActionResult<Course>> Create(CreateCourseRequest request)
    {
        var created = await _mediator.Send(new CreateCourseCommand(request));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateCourseRequest request)
    {
        await _mediator.Send(new UpdateCourseCommand(id, request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteCourseCommand(id));
        return NoContent();
    }
}
