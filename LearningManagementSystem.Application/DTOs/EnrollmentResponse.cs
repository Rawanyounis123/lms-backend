namespace LearningManagementSystem.Application.DTOs;

public class EnrollmentResponse
{
    public string Id { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public StudentSummary Student { get; set; } = new();
}

public class StudentSummary
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
