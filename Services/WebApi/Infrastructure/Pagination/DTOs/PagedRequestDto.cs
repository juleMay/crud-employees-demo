namespace WebApi.Infrastructure.Pagination.DTOs;

public class PagedRequestDto(int? page, int? pageSize, string? sortBy, string? sortDirection)
{
    public int Page { get; set; } = page ?? 1;
    public int PageSize { get; set; } = pageSize ?? 10;
    public string? SortBy { get; set; } = sortBy;
    public string? SortDirection { get; set; } = sortDirection;
}
