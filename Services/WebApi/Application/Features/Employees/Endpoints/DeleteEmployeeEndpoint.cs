using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;

namespace WebApi.Application.Features.Employees.Endpoints;

public class DeleteEmployeeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/redarbor/{id}", DeleteEmployee)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags("Employees");
    }

    private Task<IResult> DeleteEmployee([FromServices] IMediator _mediator, Guid id)
    {
        _mediator.Send(new DeleteEmployeeCommand(id));
        return Task.FromResult(Results.NoContent());
    }
}