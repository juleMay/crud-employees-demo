using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

public class CreateEmployeeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/redarbor", CreateEmployee)
            .Produces<EmployeeCommandResponseDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags("Employees");
    }

    private static async Task<IResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto, [FromServices] IMediator mediator)
    {
        var employee = await mediator.Send(new CreateEmployeeCommand(createEmployeeDto));
        return Results.Created($"api/redarbor/{employee.EmployeeId}", employee);
    }
}