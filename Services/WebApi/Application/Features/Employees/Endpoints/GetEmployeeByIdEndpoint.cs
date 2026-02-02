using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Application.Features.Employees.Queries;

namespace WebApi.Application.Features.Employees.Endpoints;

public class GetEmployeeByIdEndpoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/redarbor/{id}", GetEmployees)
            .Produces<GetEmployeeQueryDto>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Employees");
    }

    private static async Task<IResult> GetEmployees([FromServices] IMediator _mediator, [FromRoute] Guid id)
    {
       var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (employee is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(employee);
    } 
}