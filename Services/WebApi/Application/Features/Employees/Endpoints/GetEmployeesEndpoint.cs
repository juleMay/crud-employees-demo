using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Application.Features.Employees.Queries;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Pagination.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

public class GetEmployeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/redarbor", GetEmployees)
            .Produces<PagedList<GetEmployeeQueryDto>>()
            .WithTags("Employees");
    }

    private Task<PagedList<GetEmployeeQueryDto>> GetEmployees(
        [FromServices] IMediator _mediator,
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDirection) => _mediator.Send(new GetEmployeesQuery(new PagedRequestDto(page, pageSize, sortBy, sortDirection)));
}
