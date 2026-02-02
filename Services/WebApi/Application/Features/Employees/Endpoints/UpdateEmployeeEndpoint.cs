using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

public class UpdateEmployeeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/redarbor/{id}", UpdateEmployee)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithTags("Employees");
    }

    private Task UpdateEmployee([FromServices] IMediator _mediator, [FromRoute] Guid id, [FromBody] CreateEmployeeDto createEmployeeDto) => _mediator.Send(new UpdateEmployeeCommand(id, createEmployeeDto));
}