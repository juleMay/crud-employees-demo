using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Employees.Commands;

namespace WebApi.Application.Features.Employees.Endpoints;

[ApiController]
[Authorize]
[Route("api/redarbor/{id}")]
public class DeleteEmployeeController(IMediator _mediator) : ControllerBase
{
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IResult> DeleteEmployee(Guid id)
    {
        _mediator.Send(new DeleteEmployeeCommand(id));
        return Task.FromResult(Results.NoContent());
    }
}