namespace LearningManagementSystem.Application.DTOs;

public class UpdateCourseRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
    public int Credits { get; set; }
}
