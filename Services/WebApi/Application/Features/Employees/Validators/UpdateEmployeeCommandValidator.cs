using FluentValidation;
using WebApi.Application.Features.Employees.Commands;
using WebApi.Domain.Enums;

namespace WebApi.Application.Features.Employees.Validators;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithMessage("EmployeeId is required");
        When(x => x.EmployeeDto.Name != null, () =>
        {
            RuleFor(x => x.EmployeeDto.Name)
                .NotEmpty().WithMessage("Employee name must not be empty")
                .MaximumLength(100).WithMessage("Employee name must not exceed 100 characters");
        });
        When(x => x.EmployeeDto.Telephone != null, () =>
        {
            RuleFor(x => x.EmployeeDto.Telephone)
                .NotEmpty().WithMessage("Employee telephone must not be empty")
                .MaximumLength(20).WithMessage("Employee telephone must not exceed 20 characters");
        });
        When(x => x.EmployeeDto.Fax != null, () =>
        {
            RuleFor(x => x.EmployeeDto.Fax)
                .NotEmpty().WithMessage("Employee fax must not be empty")
                .MaximumLength(20).WithMessage("Employee fax must not exceed 20 characters");
        });
        When(x => x.EmployeeDto.LastLogin != null, () =>
        {
            RuleFor(x => x.EmployeeDto.LastLogin)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Last login cannot be in the future");
        });
        When(x => x.EmployeeDto.UpdatedOn != null, () =>
        {
            RuleFor(x => x.EmployeeDto.UpdatedOn)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Updated on cannot be in the future");
        });
        When(x => x.EmployeeDto.StatusId == EmployeeStatus.Inactive, () =>
        {
            RuleFor(x => x.EmployeeDto.DeletedOn)
                .NotEmpty().WithMessage("Deleted on is required when status is Inactive")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Deleted on cannot be in the future");
        });
        RuleFor(x => x.EmployeeDto.Email)
            .NotEmpty().WithMessage("Employee email is required")
            .EmailAddress().WithMessage("Employee email must be a valid email address");
        RuleFor(x => x.EmployeeDto.Username)
            .NotEmpty().WithMessage("Employee username is required")
            .MaximumLength(50).WithMessage("Employee username must not exceed 50 characters");
        RuleFor(x => x.EmployeeDto.Password)
            .NotEmpty().WithMessage("Employee password is required")
            .MinimumLength(6).WithMessage("Employee password must be at least 6 characters long")
            .MaximumLength(16).WithMessage("Employee password must not exceed 16 characters");
        RuleFor(x => x.EmployeeDto.PortalId)
            .NotEmpty().WithMessage("Portal Id is required");
        RuleFor(x => x.EmployeeDto.RoleId)
            .NotEmpty().WithMessage("Role Id is required");
        RuleFor(x => x.EmployeeDto.StatusId)
            .NotNull().WithMessage("Status Id is required")
            .IsInEnum().WithMessage("Status Id must be a valid enum value");
        RuleFor(x => x.EmployeeDto.CompanyId)
            .NotEmpty().WithMessage("Company Id is required");
    }
}
