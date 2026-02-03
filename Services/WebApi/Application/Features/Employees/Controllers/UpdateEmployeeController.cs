using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Endpoints;

[ApiController]
[Authorize]
[Route("api/redarbor/{id}")]
public class UpdateEmployeeController(IMediator _mediator) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public Task UpdateEmployee([FromRoute] Guid id, [FromBody] CreateEmployeeDto createEmployeeDto) => _mediator.Send(new UpdateEmployeeCommand(id, createEmployeeDto));
}