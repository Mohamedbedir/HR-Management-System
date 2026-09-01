using FluentValidation;
using HR.Core.Features.Employees.Commands.Models;
using HR.Core.Localization;
using HR.Service.Services.Contract;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Commands.Validators
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeCommand>
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly IPositionService _positionService;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public AddEmployeeValidator(
        IEmployeeService employeeService,
        IDepartmentService departmentService,
        IPositionService positionService,
        IStringLocalizer<SharedResources> localizer)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _positionService = positionService;
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    private void ApplyValidationRules()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^01[0125][0-9]{8}$");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.TerminationDate)
            .GreaterThanOrEqualTo(x => x.HireDate)
            .When(x => x.TerminationDate.HasValue)
            .WithMessage("Termination date must be greater than or equal to hire date.");

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .When(x => x.Gender.HasValue);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .When(x => x.DepartmentId.HasValue);

        RuleFor(x => x.PositionId)
            .GreaterThan(0)
            .When(x => x.PositionId.HasValue);

        RuleFor(x => x.ManagerId)
            .GreaterThan(0)
            .When(x => x.ManagerId.HasValue);
    }

    private void ApplyCustomValidationRules()
    {
        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellationToken) =>
                !await _employeeService.IsEmailExist(email))
            .WithMessage("Email already exists.");

        RuleFor(x => x.Phone)
            .MustAsync(async (phone, cancellationToken) =>
                !await _employeeService.IsPhoneExist(phone))
            .WithMessage("Phone already exists.");

        RuleFor(x => x.DepartmentId)
            .MustAsync(async (departmentId, cancellationToken) =>
                departmentId == null ||
                await _departmentService.IsDepartmentExistById(departmentId.Value))
            .WithMessage("Department does not exist.");

        RuleFor(x => x.PositionId)
            .MustAsync(async (positionId, cancellationToken) =>
                positionId == null ||
                await _positionService.IsPositionExistById(positionId.Value))
            .WithMessage("Position does not exist.");

        RuleFor(x => x.ManagerId)
            .MustAsync(async (managerId, cancellationToken) =>
                managerId == null ||
                await _employeeService.IsEmployeeExistById(managerId.Value))
            .WithMessage("Manager does not exist.");
    }
}
}
