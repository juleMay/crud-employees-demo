
using MediatR;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Queries;

public class GetEmployeeByIdQuery(Guid employeeId) : IRequest<GetEmployeeQueryDto>
{
    public Guid EmployeeId { get; set; } = employeeId;
}
