using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Application.Features.Employees.Queries;

namespace WebApi.Application.Features.Employees.Endpoints;

[ApiController]
[Authorize]
[Route("api/redarbor/{id}")]
public class GetEmployeeByIdController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetEmployeeQueryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetEmployee([FromRoute] Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (employee is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(employee);
    }
}