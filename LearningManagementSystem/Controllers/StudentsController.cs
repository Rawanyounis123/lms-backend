using LearningManagementSystem.Application.DTOs;
using LearningManagementSystem.Application.Features.Students.Commands;
using LearningManagementSystem.Application.Features.Students.Queries;
using LearningManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<PagedResult<Student>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetAllStudentsQuery(page, pageSize)));

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(string id) =>
        Ok(await _mediator.Send(new GetStudentByIdQuery(id)));

    [HttpPost]
    public async Task<ActionResult<Student>> Create(CreateStudentRequest request)
    {
        var created = await _mediator.Send(new CreateStudentCommand(request));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateStudentRequest request)
    {
        await _mediator.Send(new UpdateStudentCommand(id, request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteStudentCommand(id));
        return NoContent();
    }
}
