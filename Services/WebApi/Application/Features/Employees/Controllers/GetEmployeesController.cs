using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Application.Features.Employees.Queries;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Pagination.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

[ApiController]
[Authorize]
[Route("api/redarbor")]
public class GetEmployeesController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<GetEmployeeQueryDto>), StatusCodes.Status200OK)]
    public Task<PagedList<GetEmployeeQueryDto>> GetEmployees(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDirection) => _mediator.Send(new GetEmployeesQuery(new PagedRequestDto(page, pageSize, sortBy, sortDirection)));
}
