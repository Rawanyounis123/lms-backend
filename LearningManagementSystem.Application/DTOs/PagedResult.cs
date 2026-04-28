namespace LearningManagementSystem.Application.DTOs;

public class PagedResult<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public List<T> Items { get; set; } = [];
}
