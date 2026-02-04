using WebApi.Application.Features.Employees.Commands.Services.Contracts;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Exceptions;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Application.Features.Employees.Commands.Services;

public class EmployeeCommandService(IUnitOfWork _unitOfWork) : IEmployeeCommandService
{
    public async Task<Employee> Create(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await ValidateUserCreation(request.EmployeeDto.Username, request.EmployeeDto.Email, cancellationToken);
        var user = User.Create(request.EmployeeDto);
        var employee = Employee.Create(request.EmployeeDto, user);
        await EnrollUser(request.EmployeeDto.PortalId, user);
        await AuthorizeUser(request.EmployeeDto.RoleId, user);
        await HireEmployee(request.EmployeeDto.CompanyId, employee);
        await _unitOfWork.UserRepository.InsertAsync(user, cancellationToken);
        await _unitOfWork.EmployeeRepository.InsertAsync(employee, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        return employee;
    }

    private async Task ValidateUserCreation(string username, string email, CancellationToken cancellationToken)
    {
        List<string> errors = [];
        if (await _unitOfWork.UserRepository.ExistsByUserNameAsync(username, cancellationToken))
        {
            errors.Add($"Username '{username}' is already taken");
        }
        if (await _unitOfWork.UserRepository.ExistsByEmailAsync(Email.Create(email), cancellationToken))
        {
            errors.Add($"Email '{email}' is already registered");
        }
        if (errors.Count > 0)
        {
            throw new ValidationAppException(errors);
        }
    }

    private async Task AuthorizeUser(Guid roleId, User user)
    {
        var role = await _unitOfWork.RoleGenericRepository.GetByIdAsync(roleId);
        if (role is null)
        {
            throw new NotFoundAppException(nameof(Role), roleId);
        }
        else
        {
            role.AuthorizeUser(user);
            _unitOfWork.RoleGenericRepository.Update(role);
        }
    }

    private async Task EnrollUser(Guid portalId, User user)
    {
        var portal = await _unitOfWork.PortalGenericRepository.GetByIdAsync(portalId);
        if (portal is null)
        {
            throw new NotFoundAppException(nameof(Portal), portalId);
        }
        else
        {
            portal.EnrollUser(user);
            _unitOfWork.PortalGenericRepository.Update(portal);
        }
    }

    private async Task HireEmployee(Guid companyId, Employee employee)
    {
        var company = await _unitOfWork.CompanyGenericRepository.GetByIdAsync(companyId);
        if (company is null)
        {
            throw new NotFoundAppException(nameof(Company), companyId);
        }
        else
        {
            company.HireEmployee(employee);
            _unitOfWork.CompanyGenericRepository.Update(company);
        }
    }

    public async Task Update(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await UpdateEmployee(request, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }

    private async Task UpdateEmployee(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetIncludeUserByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is not null)
        {
            UpdateEmployeeStatus(employee, request.EmployeeDto.StatusId, request.EmployeeDto.DeletedOn);
            employee.Update(request.EmployeeDto);
            _unitOfWork.EmployeeRepository.Update(employee);
        }
        else
        {
            throw new NotFoundAppException(nameof(employee), request.EmployeeId);
        }
        await UpdateUser(employee.User, employee, request, cancellationToken);
    }

    private static void UpdateEmployeeStatus(Employee employee, EmployeeStatus newStatus, DateTime? deletedOn)
    {
        if (employee.Status == newStatus)
        {
            return;
        }
        if (employee.Status == EmployeeStatus.Inactive && employee.DeletedOn is not null)
        {
            if (newStatus == EmployeeStatus.Reinstated)
            {
                employee.Reinstate();
            }
            else
            {
                throw new NotFoundAppException(nameof(employee), employee.Id);
            }
        }
        else if (newStatus == EmployeeStatus.Inactive)
        {
            employee.Fire(deletedOn);
        }
    }

    private async Task UpdateUser(User user, Employee employee, UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await ValidateUserUpdate(employee.User, request.EmployeeDto.Email, request.EmployeeDto.Username, cancellationToken);
        if (user is not null)
        {
            user.Update(request.EmployeeDto);
            _unitOfWork.UserRepository.Update(user);
        }
        else
        {
            throw new NotFoundAppException(nameof(user), employee.UserId);
        }
    }

    public async Task ValidateUserUpdate(User user, string email, string username, CancellationToken cancellationToken)
    {
        List<string> errors = [];
        if (await _unitOfWork.UserRepository.ExistsByUserNameAsync(username, user.Id, cancellationToken))
        {
            errors.Add($"Username '{username}' is already taken");
        }
        if (await _unitOfWork.UserRepository.ExistsByEmailAsync(Email.Create(email), user.Id, cancellationToken))
        {
            errors.Add($"Email '{email}' is already registered");
        }
        if (errors.Count > 0)
        {
            throw new ValidationAppException(errors);
        }
    }
}
