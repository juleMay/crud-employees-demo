using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

[ApiController]
[Authorize]
[Route("api/redarbor")]
public class CreateEmployeeController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeCommandResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _mediator.Send(new CreateEmployeeCommand(createEmployeeDto));
        return Results.Created($"api/redarbor/{employee.EmployeeId}", employee);
    }
}